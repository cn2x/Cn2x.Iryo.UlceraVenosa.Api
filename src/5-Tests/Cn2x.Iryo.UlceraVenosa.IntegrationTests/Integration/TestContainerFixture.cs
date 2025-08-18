using System;
using System.Threading.Tasks;
using Xunit;
using Testcontainers.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Http;
using Npgsql;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class TestContainerFixture : IAsyncLifetime
{
	private PostgreSqlContainer? _postgresContainer;
	public string ConnectionString { get; private set; } = string.Empty;

	public async Task InitializeAsync()
	{
		// Gera um nome único para o banco de dados
		var dbName = $"testdb_{Guid.NewGuid():N}";
		
		// Configura e inicia o container PostgreSQL
		_postgresContainer = new PostgreSqlBuilder()
			.WithImage("postgres:15")
			.WithDatabase(dbName)
			.WithUsername("testuser")
			.WithPassword("testpass")
			.WithCleanUp(true)
			.WithName($"test_postgres_{Guid.NewGuid():N}")
			.Build();

		await _postgresContainer.StartAsync();

		// Obtém a string de conexão do container
		ConnectionString = _postgresContainer.GetConnectionString();

		Console.WriteLine($"[TestContainerFixture] Container PostgreSQL iniciado. Database: {dbName}, ConnectionString: {ConnectionString}");

		// Testa a conexão
		var maxAttempts = 5;
		var delayMs = 2000;

		for (int i = 0; i < maxAttempts; i++)
		{
			try
			{
				// Testa a conexão usando Npgsql diretamente
				using var connection = new NpgsqlConnection(ConnectionString);
				await connection.OpenAsync();
				
				Console.WriteLine($"[TestContainerFixture] Conexão com container estabelecida após {i + 1} tentativas.");
				break;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[TestContainerFixture] Tentativa {i + 1} de conexão falhou: {ex.Message}");
				if (i == maxAttempts - 1)
				{
					throw new InvalidOperationException($"Não foi possível conectar ao container PostgreSQL após {maxAttempts} tentativas.", ex);
				}
				await Task.Delay(delayMs);
			}
		}

		// Executa as migrações do Entity Framework para criar as tabelas
		await ExecutarMigracoesAsync();

		Console.WriteLine("[TestContainerFixture] Container PostgreSQL configurado com sucesso.");
	}

	private async Task ExecutarMigracoesAsync()
	{
		try
		{
			Console.WriteLine("[TestContainerFixture] Executando migrações (suprimindo PendingModelChangesWarning)...");

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder
				.UseNpgsql(ConnectionString)
				.ConfigureWarnings(w => w.Log(RelationalEventId.PendingModelChangesWarning));

			var mediatorMock = new Mock<IMediator>();
			var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

			using var db = new ApplicationDbContext(optionsBuilder.Options, mediatorMock.Object, httpContextAccessorMock.Object);
			await db.Database.MigrateAsync();
			Console.WriteLine("[TestContainerFixture] Migrações aplicadas com sucesso.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[TestContainerFixture] Erro ao executar migrações: {ex.Message}");
			throw;
		}
	}

	public async Task DisposeAsync()
	{
		if (_postgresContainer != null)
		{
			await _postgresContainer.DisposeAsync();
			Console.WriteLine("[TestContainerFixture] Container PostgreSQL finalizado.");
		}
	}
}
