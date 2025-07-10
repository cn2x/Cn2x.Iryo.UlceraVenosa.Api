using System.Threading.Tasks;
using Xunit;
using Testcontainers.PostgreSql;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class PostgresContainerBasicTest : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithDatabase("testdb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
    }

    [Fact]
    public async Task DeveSubirEConectarNoPostgresContainer()
    {
        // Arrange
        var connectionString = _postgresContainer.GetConnectionString();

        // Act
        await using var conn = new Npgsql.NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        // Assert
        Assert.Equal(System.Data.ConnectionState.Open, conn.State);
    }
}
