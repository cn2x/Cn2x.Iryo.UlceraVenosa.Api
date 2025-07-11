using Xunit;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;
using Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{

    public class CustomWebApplicationFactory : WebApplicationFactory<Program> {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureAppConfiguration((context, configBuilder) => {
                configBuilder.AddInMemoryCollection(new Dictionary<string, string?> {
                    ["ConnectionStrings:DefaultConnection"] = _connectionString
                });
            });

            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(_connectionString));

                // Migrate and seed
                using var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();

                TestSeedData.Seed(db);
            });
        }
    }

    public class RepositoryIntegrationTest : IClassFixture<DatabaseFixture> 
    {  
        private ApplicationDbContext _dbContext;
        private readonly HttpClient _client;
        public RepositoryIntegrationTest(DatabaseFixture dbFixture) {
            var factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Persistir_E_Consultar_Ulcera()
        {
            // Arrange
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("55555555-aaaa-bbbb-cccc-111111111111");
            var segmentacaoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var regiaoAnatomicaId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var topografia = new TopografiaPerna {
                LateralidadeId = lateralidadeId,
                SegmentacaoId = segmentacaoId,
                RegiaoAnatomicaId = regiaoAnatomicaId
            };
            var ulcera = new Ulcera { PacienteId = pacienteId, Topografia = topografia };
            // Act
            _dbContext.Ulceras.Add(ulcera);
            await _dbContext.SaveChangesAsync();
            var ulceraDb = await _dbContext.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            // Assert
            Assert.NotNull(ulceraDb);
            Assert.Equal(pacienteId, ulceraDb.PacienteId);
        }

        [Fact]
        public async Task Deve_Atualizar_Ulcera_Ceap()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            var lateralidadeId = Guid.Parse("55555555-aaaa-bbbb-cccc-111111111111");
            var segmentacaoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var regiaoAnatomicaId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var topografia = new TopografiaPerna
            {
                LateralidadeId = lateralidadeId,
                SegmentacaoId = segmentacaoId,
                RegiaoAnatomicaId = regiaoAnatomicaId
            };
            var ulcera = new Ulcera { PacienteId = pacienteId, Topografia = topografia, Ceap = new Ceap(Clinica.UlceraAtiva, Etiologica.Primaria, Anatomica.Profundo, Patofisiologica.Refluxo) };
            _dbContext.Ulceras.Add(ulcera);
            await _dbContext.SaveChangesAsync();
            // Act
            var ulceraDb = await _dbContext.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraDb);
            ulceraDb.Ceap = new Ceap(Clinica.UlceraCicatrizada, Etiologica.Secundaria, Anatomica.Superficial, Patofisiologica.Obstrucao);
            await _dbContext.SaveChangesAsync();
            // Assert
            var ulceraAtualizada = await _dbContext.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraAtualizada);
            Assert.True(ulceraAtualizada.Ceap.ClasseClinica.Equals(Clinica.UlceraCicatrizada));
            Assert.True(ulceraAtualizada.Ceap.Etiologia.Equals(Etiologica.Secundaria));
            Assert.True(ulceraAtualizada.Ceap.Anatomia.Equals(Anatomica.Superficial));
            Assert.True(ulceraAtualizada.Ceap.Patofisiologia.Equals(Patofisiologica.Obstrucao));
        }

        [Fact]
        public async Task Deve_Deletar_Ulcera()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            var lateralidadeId = Guid.Parse("55555555-aaaa-bbbb-cccc-111111111111");
            var segmentacaoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var regiaoAnatomicaId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var topografia = new TopografiaPerna
            {
                LateralidadeId = lateralidadeId,
                SegmentacaoId = segmentacaoId,
                RegiaoAnatomicaId = regiaoAnatomicaId
            };
            var ulcera = new Ulcera { PacienteId = pacienteId, Topografia = topografia };
            _dbContext.Ulceras.Add(ulcera);
            await _dbContext.SaveChangesAsync();
            // Act
            var ulceraDb = await _dbContext.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraDb);
            _dbContext.Ulceras.Remove(ulceraDb);
            await _dbContext.SaveChangesAsync();
            // Assert
            var ulceraRemovida = await _dbContext.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.Null(ulceraRemovida);
        }

        [Fact]
        public async Task Deve_Persistir_E_Consultar_Ulcera_Com_Seed_Ids_E_Command()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            // Busca os IDs seedados
            var lateralidade = await _dbContext.Lateralidades.FirstAsync(l => l.Nome == "Direita");
            var segmentacao = await _dbContext.Set<Segmentacao>().FirstAsync(s => s.Sigla == "TS");
            var regiao = await _dbContext.Set<RegiaoAnatomica>().FirstAsync(r => r.Sigla == "M");
            // Cria a topografia perna usando os IDs seedados
            var topografia = new TopografiaPerna
            {
                LateralidadeId = lateralidade.Id,
                Lateralidade = lateralidade,
                SegmentacaoId = segmentacao.Id,
                Segmentacao = segmentacao,
                RegiaoAnatomicaId = regiao.Id,
                RegiaoAnatomica = regiao
            };
            _dbContext.TopografiasPerna.Add(topografia);
            await _dbContext.SaveChangesAsync();
            // Monta o comando
            var command = new UpsertUlceraCommand
            {
                PacienteId = pacienteId,
                TipoTopografia = TopografiaEnum.Perna,
                TopografiaId = topografia.Id,
                ClassificacaoCeap = null
            };
            var handler = new UpsertUlceraCommandHandler(new UlceraRepository(_dbContext), _dbContext);
            // Act
            var ulceraId = await handler.Handle(command, default);
            var ulceraDb = await _dbContext.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.Id == ulceraId);
            // Assert
            Assert.NotNull(ulceraDb);
            Assert.Equal(pacienteId, ulceraDb.PacienteId);
            Assert.Equal(topografia.Id, ulceraDb.TopografiaId);
        }
    }
}
