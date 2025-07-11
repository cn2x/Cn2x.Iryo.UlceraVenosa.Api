using System.Threading.Tasks;
using Xunit;
using Testcontainers.PostgreSql;

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
    }

    public async Task DisposeAsync() {
        await Container.DisposeAsync();
    }
}
