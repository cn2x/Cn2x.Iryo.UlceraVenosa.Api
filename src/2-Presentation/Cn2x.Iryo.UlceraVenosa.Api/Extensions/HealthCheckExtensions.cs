using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Cn2x.Iryo.UlceraVenosa.Api.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var healthChecksSection = configuration.GetSection("HealthChecks");
        
        // Configurações do PostgreSQL
        var postgresqlTimeout = TimeSpan.Parse(healthChecksSection.GetValue<string>("PostgreSQL:Timeout", "00:00:10"));
        
        // Configurações de memória
        var maxMemoryMB = healthChecksSection.GetValue<int>("Memory:MaximumMegabytesAllocated", 1024);
        
        // Configurações de disco
        var minDiskSpaceGB = healthChecksSection.GetValue<int>("Disk:MinimumFreeSpaceGB", 1);
        
        // Configurações da aplicação
        var maxAppMemoryMB = healthChecksSection.GetValue<int>("Application:MaximumMemoryMB", 500);
        
        services.AddHealthChecks()
            // Health check para PostgreSQL - usando health check customizado para mais controle
            .AddCheck<PostgreSQLHealthCheck>(
                name: "postgresql",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "database", "postgresql" })
            // Health check para memória
            .AddCheck<MemoryHealthCheck>(
                name: "memory",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "system", "memory" })
            // Health check para disco
            .AddCheck<DiskHealthCheck>(
                name: "disk",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "system", "disk" })
            // Health check customizado para a aplicação
            .AddCheck<ApplicationHealthCheck>(
                name: "application",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "application" });

        return services;
    }

    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            AllowCachingResponses = false
        });

        app.UseHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("database"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            AllowCachingResponses = false
        });

        app.UseHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false, // Não executa health checks para liveness
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            AllowCachingResponses = false
        });

        return app;
    }
}

// Health check customizado para PostgreSQL
public class PostgreSQLHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PostgreSQLHealthCheck> _logger;

    public PostgreSQLHealthCheck(IConfiguration configuration, ILogger<PostgreSQLHealthCheck> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var timeout = TimeSpan.Parse(_configuration.GetValue<string>("HealthChecks:PostgreSQL:Timeout", "00:00:10"));

        if (string.IsNullOrEmpty(connectionString))
        {
            _logger.LogWarning("Connection string não configurada para PostgreSQL");
            return HealthCheckResult.Unhealthy("Connection string não configurada");
        }

        try
        {
            _logger.LogInformation("Iniciando health check do PostgreSQL");
            
            using var connection = new NpgsqlConnection(connectionString);
            using var cts = new CancellationTokenSource(timeout);
            
            _logger.LogInformation("Tentando abrir conexão com PostgreSQL...");
            await connection.OpenAsync(cts.Token);
            _logger.LogInformation("Conexão aberta com sucesso");
            
            // Executa uma query simples para verificar se o banco está respondendo
            using var command = new NpgsqlCommand("SELECT 1", connection);
            command.CommandTimeout = (int)timeout.TotalSeconds;
            
            _logger.LogInformation("Executando query de teste...");
            var result = await command.ExecuteScalarAsync(cts.Token);
            _logger.LogInformation("Query executada com sucesso: {Result}", result);
            
            var data = new Dictionary<string, object>
            {
                { "Database", connection.Database },
                { "ServerVersion", connection.ServerVersion },
                { "State", connection.State.ToString() },
                { "Timeout", timeout.TotalSeconds }
            };

            return HealthCheckResult.Healthy("Conexão com PostgreSQL estabelecida com sucesso", data);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(ex, "Timeout ao conectar com PostgreSQL após {Timeout} segundos", timeout.TotalSeconds);
            return HealthCheckResult.Degraded("Timeout ao conectar com PostgreSQL");
        }
        catch (NpgsqlException ex)
        {
            _logger.LogError(ex, "Erro de Npgsql ao conectar com PostgreSQL: {ErrorCode} - {Message}", ex.ErrorCode, ex.Message);
            return HealthCheckResult.Degraded($"Erro de conexão com PostgreSQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao verificar PostgreSQL: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
            return HealthCheckResult.Unhealthy("Erro inesperado ao verificar PostgreSQL", ex);
        }
    }
}

// Health check para memória
public class MemoryHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public MemoryHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var process = System.Diagnostics.Process.GetCurrentProcess();
            var memoryMB = process.WorkingSet64 / (1024 * 1024);
            var maxMemoryMB = _configuration.GetValue<int>("HealthChecks:Memory:MaximumMegabytesAllocated", 1024);

            var data = new Dictionary<string, object>
            {
                { "MemoryUsageMB", memoryMB },
                { "MaxMemoryMB", maxMemoryMB },
                { "ProcessId", process.Id }
            };

            if (memoryMB > maxMemoryMB)
            {
                return Task.FromResult(HealthCheckResult.Degraded(
                    description: $"Uso de memória alto: {memoryMB}MB (máximo: {maxMemoryMB}MB)",
                    data: data));
            }

            return Task.FromResult(HealthCheckResult.Healthy(
                description: $"Uso de memória normal: {memoryMB}MB",
                data: data));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                description: "Erro ao verificar uso de memória",
                exception: ex));
        }
    }
}

// Health check para disco
public class DiskHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public DiskHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var minDiskSpaceGB = _configuration.GetValue<int>("HealthChecks:Disk:MinimumFreeSpaceGB", 1);
            var minDiskSpaceBytes = minDiskSpaceGB * 1024L * 1024L * 1024L;

            var drive = new DriveInfo("C");
            var freeSpaceBytes = drive.AvailableFreeSpace;
            var freeSpaceGB = freeSpaceBytes / (1024L * 1024L * 1024L);

            var data = new Dictionary<string, object>
            {
                { "FreeSpaceGB", freeSpaceGB },
                { "MinFreeSpaceGB", minDiskSpaceGB },
                { "TotalSizeGB", drive.TotalSize / (1024L * 1024L * 1024L) },
                { "DriveName", drive.Name }
            };

            if (freeSpaceBytes < minDiskSpaceBytes)
            {
                return Task.FromResult(HealthCheckResult.Degraded(
                    description: $"Espaço em disco baixo: {freeSpaceGB}GB livre (mínimo: {minDiskSpaceGB}GB)",
                    data: data));
            }

            return Task.FromResult(HealthCheckResult.Healthy(
                description: $"Espaço em disco adequado: {freeSpaceGB}GB livre",
                data: data));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                description: "Erro ao verificar espaço em disco",
                exception: ex));
        }
    }
}

// Health check customizado para a aplicação
public class ApplicationHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public ApplicationHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Verificações básicas da aplicação
            var totalMemory = GC.GetTotalMemory(false);
            var maxAppMemoryMB = _configuration.GetValue<int>("HealthChecks:Application:MaximumMemoryMB", 500);
            var maxAppMemoryBytes = maxAppMemoryMB * 1024 * 1024;
            
            var data = new Dictionary<string, object>
            {
                { "TotalMemory", totalMemory },
                { "MaxMemoryMB", maxAppMemoryMB },
                { "GCGeneration0", GC.CollectionCount(0) },
                { "GCGeneration1", GC.CollectionCount(1) },
                { "GCGeneration2", GC.CollectionCount(2) },
                { "ThreadCount", ThreadPool.ThreadCount },
                { "Uptime", Environment.TickCount64 },
                { "ProcessId", Environment.ProcessId },
                { "MachineName", Environment.MachineName },
                { "OSVersion", Environment.OSVersion.ToString() },
                { "ProcessorCount", Environment.ProcessorCount },
                { "Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown" }
            };

            // Verifica se a memória está em níveis aceitáveis
            if (totalMemory > maxAppMemoryBytes)
            {
                return Task.FromResult(HealthCheckResult.Degraded(
                    description: $"Aplicação usando muita memória: {totalMemory / (1024 * 1024)}MB (máximo: {maxAppMemoryMB}MB)",
                    data: data));
            }

            return Task.FromResult(HealthCheckResult.Healthy(
                description: "Aplicação funcionando normalmente",
                data: data));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                description: "Erro ao verificar saúde da aplicação",
                exception: ex));
        }
    }
}