using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Priority;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{
    [Collection("Ordered")]
    [TestCaseOrderer("Xunit.Priority.Orderers.PriorityOrderer", "Xunit.Priority")]
    public class RepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly CustomWebApplicationFactory _factory;

        public RepositoryIntegrationTest(DatabaseFixture dbFixture)
        {
            _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
        }

        [Fact, Priority(1)]
        public async Task Deve_Persistir_E_Consultar_Ulcera()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (db == null) throw new InvalidOperationException("DbContext não inicializado");

            // Garante que o paciente do seed existe
            TestSeedData.Seed(db);
            var pacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId;
            var lateralidadeId = Guid.Parse("55555555-aaaa-bbbb-cccc-111111111111");
            var segmentacaoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var regiaoAnatomicaId = Guid.Parse("44444444-4444-4444-4444-444444444444");

            var topografia = new TopografiaPerna {
                LateralidadeId = lateralidadeId,
                SegmentacaoId = segmentacaoId,
                RegiaoAnatomicaId = regiaoAnatomicaId
            };

            var ceap = new Ceap(
                Clinica.SemSinais,
                Etiologica.Primaria,
                Anatomica.Profundo,
                Patofisiologica.Refluxo
            );

            var ulcera = new Ulcera { PacienteId = pacienteId, Topografia = topografia, Ceap = ceap };
            db.Ulceras.Add(ulcera);
            await db.SaveChangesAsync();
            var ulceraDb = await db.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraDb);
            Assert.Equal(pacienteId, ulceraDb.PacienteId);
        }

        [Fact, Priority(2)]
        public async Task Deve_Atualizar_Ulcera_Ceap()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (db == null) throw new InvalidOperationException("DbContext não inicializado");

            // Garante que o paciente do seed existe
            Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(db);
            var pacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId;
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
            db.Ulceras.Add(ulcera);
            await db.SaveChangesAsync();
            var ulceraDb = await db.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraDb);
            ulceraDb.Ceap = new Ceap(Clinica.UlceraCicatrizada, Etiologica.Secundaria, Anatomica.Superficial, Patofisiologica.Obstrucao);
            await db.SaveChangesAsync();
            var ulceraAtualizada = await db.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraAtualizada);
            Assert.True(ulceraAtualizada.Ceap.ClasseClinica.Equals(Clinica.UlceraCicatrizada));
            Assert.True(ulceraAtualizada.Ceap.Etiologia.Equals(Etiologica.Secundaria));
            Assert.True(ulceraAtualizada.Ceap.Anatomia.Equals(Anatomica.Superficial));
            Assert.True(ulceraAtualizada.Ceap.Patofisiologia.Equals(Patofisiologica.Obstrucao));
        }

        [Fact, Priority(3)]
        public async Task Deve_Deletar_Ulcera()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (db == null) throw new InvalidOperationException("DbContext não inicializado");

            // Garante que o paciente do seed existe
            Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(db);
            var pacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId;
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
            db.Ulceras.Add(ulcera);
            await db.SaveChangesAsync();
            var ulceraDb = await db.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.NotNull(ulceraDb);
            db.Ulceras.Remove(ulceraDb);
            await db.SaveChangesAsync();
            var ulceraRemovida = await db.Ulceras.FirstOrDefaultAsync(u => u.PacienteId == pacienteId);
            Assert.Null(ulceraRemovida);
        }
    }
}
