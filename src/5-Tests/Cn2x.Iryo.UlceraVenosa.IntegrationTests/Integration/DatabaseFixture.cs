using System.Threading.Tasks;
using Xunit;
using Testcontainers.PostgreSql;
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

public class DatabaseFixture : IAsyncLifetime {
    public PostgreSqlContainer Container { get; private set; } = default!;
    public string ConnectionString => Container.GetConnectionString();

    public async Task InitializeAsync() {
        Container = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .Build();

        await Container.StartAsync();
        // Aguarda até o banco estar realmente pronto para conexões
        var maxAttempts = 10;
        var delayMs = 1000;
        var npgsqlConn = new NpgsqlConnection(Container.GetConnectionString());
        for (int i = 0; i < maxAttempts; i++)
        {
            try
            {
                await npgsqlConn.OpenAsync();
                await npgsqlConn.CloseAsync();
                Console.WriteLine($"[DatabaseFixture] Banco pronto após {i + 1} tentativas.");
                break;
            }
            catch
            {
                Console.WriteLine($"[DatabaseFixture] Tentativa {i + 1} de conexão falhou, aguardando...");
                await Task.Delay(delayMs);
            }
        }

        // Aplica as migrations automaticamente
        var optionsBuilder = new DbContextOptionsBuilder<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
        optionsBuilder.UseNpgsql(Container.GetConnectionString());
        var mediatorMock = new Moq.Mock<MediatR.IMediator>();
        var httpContextAccessorMock = new Moq.Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        using var db = new Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext(optionsBuilder.Options, mediatorMock.Object, httpContextAccessorMock.Object);
        db.Database.Migrate();
    }

    public async Task DisposeAsync() {
        await Container.DisposeAsync();
    }
}
