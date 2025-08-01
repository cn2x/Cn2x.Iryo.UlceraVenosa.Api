using Microsoft.AspNetCore.ResponseCompression;
using System.Runtime;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;
using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Api.Extensions;

/// <summary>
/// Extensões para configuração de serviços da API
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona todos os serviços da aplicação
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomMassTransit(configuration);

        // Configurações de performance
        services.AddPerformanceOptimizations();
        
        // Configurações de CORS
        services.AddCorsConfiguration();
        
        // Configurações de compressão
        services.AddResponseCompression();
        
        // Configurações de cache
        services.AddMemoryCache();
        
        // Configurações de logging
        services.AddLoggingConfiguration();
        
        // Configurações de HTTP Context Accessor
        services.AddHttpContextAccessor();
        
        // Configurações de MediatR
        services.AddMediatR();
        
        // Configurações de infraestrutura
        services.AddInfrastructureServices();
        
        // Configurações de GraphQL
        // services.AddGraphQLServices();
        
        return services;
    }

    /// <summary>
    /// Configura otimizações de performance
    /// </summary>
    private static IServiceCollection AddPerformanceOptimizations(this IServiceCollection services)
    {
        // Configurações do Garbage Collector
        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
        
        // Configurações do Thread Pool
        ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
        ThreadPool.SetMaxThreads(Environment.ProcessorCount * 2, Environment.ProcessorCount * 2);
        
        return services;
    }

    /// <summary>
    /// Configura CORS
    /// </summary>
    private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowedOrigins", policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:3000",
                        "https://localhost:3000",
                        "http://localhost:9000",
                        "https://localhost:9000",
                        "https://firebasestorage.googleapis.com",
                        "https://cdn.jsdelivr.net"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }

    /// <summary>
    /// Configura compressão de resposta
    /// </summary>
    private static IServiceCollection AddResponseCompression(this IServiceCollection services)
    {
        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        return services;
    }

    /// <summary>
    /// Configura logging estruturado
    /// </summary>
    private static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            
            // Configurações de performance para logging
            builder.AddFilter("Microsoft", LogLevel.Warning);
            builder.AddFilter("System", LogLevel.Warning);
            builder.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
        });

        return services;
    }

    /// <summary>
    /// Configura MediatR para domain events
    /// </summary>
    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        // Registra MediatR real para a aplicação
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.AtivarInativarUlceraCommand).Assembly));
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // ... outros serviços ...
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        // ... outros serviços ...
        return services;
    }
} 