using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Testcontainers.PostgreSql;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{
    public class RepositoryIntegrationTest : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _pgContainer = new PostgreSqlBuilder()
            .WithDatabase("ulcera_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
        private ApplicationDbContext _dbContext;

        public async Task InitializeAsync()
        {
            await _pgContainer.StartAsync();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(_pgContainer.GetConnectionString())
                .Options;
            var mediatorMock = new Mock<IMediator>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _dbContext = new ApplicationDbContext(options, mediatorMock.Object, httpContextAccessorMock.Object);
            await _dbContext.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _pgContainer.DisposeAsync();
        }

        [Fact]
        public async Task Deve_Persistir_E_Consultar_Ulcera()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            var topografia = new TopografiaPerna
            {
                Lateralidade = new Lateralidade { Nome = "Direita" },
                Segmentacao = new Segmentacao { Sigla = "TS", Descricao = "desc" },
                RegiaoAnatomica = new RegiaoAnatomica { Sigla = "M", Descricao = "Medial" }
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
    }
}
