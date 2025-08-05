using System.Threading.Tasks;
using Xunit;
using System;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Http;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class DatabaseFixture : IAsyncLifetime 
{
    public string ConnectionString { get; private set; } = "Host=pgsql52-farm1.kinghost.net;Database=mytms2;Username=mytm2s;Password=Rickyelton10@;SSL Mode=Prefer;Trust Server Certificate=true;Timeout=60;Command Timeout=120;";

    public async Task InitializeAsync() 
    {
        // Testa a conexão com o banco KingHost
        var maxAttempts = 5;
        var delayMs = 2000;
        
        for (int i = 0; i < maxAttempts; i++)
        {
            try
            {
                using var npgsqlConn = new NpgsqlConnection(ConnectionString);
                await npgsqlConn.OpenAsync();
                await npgsqlConn.CloseAsync();
                Console.WriteLine($"[DatabaseFixture] Conexão com KingHost estabelecida após {i + 1} tentativas.");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DatabaseFixture] Tentativa {i + 1} de conexão falhou: {ex.Message}");
                if (i == maxAttempts - 1)
                {
                    throw new InvalidOperationException($"Não foi possível conectar ao banco KingHost após {maxAttempts} tentativas.", ex);
                }
                await Task.Delay(delayMs);
            }
        }

        // Aplica as migrations automaticamente
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(ConnectionString, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null);
            
            npgsqlOptions.CommandTimeout(60);
        });
        
        var mediatorMock = new Mock<IMediator>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        
        using var db = new ApplicationDbContext(optionsBuilder.Options, mediatorMock.Object, httpContextAccessorMock.Object);
        await db.Database.MigrateAsync();
        Console.WriteLine("[DatabaseFixture] Migrations aplicadas com sucesso.");
    }

    public async Task DisposeAsync() 
    {
        // Não há nada para limpar quando usando banco remoto
        await Task.CompletedTask;
    }
}

