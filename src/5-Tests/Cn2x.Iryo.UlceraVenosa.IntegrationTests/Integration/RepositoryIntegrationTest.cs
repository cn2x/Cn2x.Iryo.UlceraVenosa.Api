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
           
        }

        [Fact]
        public async Task Deve_Atualizar_Ulcera_Ceap()
        {
          
        }

        [Fact]
        public async Task Deve_Deletar_Ulcera()
        {
            // Arrange
       
        }

        [Fact]
        public async Task Deve_Persistir_E_Consultar_Ulcera_Com_Seed_Ids_E_Command()
        {
        }
    }
}
