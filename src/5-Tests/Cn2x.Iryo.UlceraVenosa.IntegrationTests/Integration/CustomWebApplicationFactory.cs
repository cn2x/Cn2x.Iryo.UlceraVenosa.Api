using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;
using Cn2x.Iryo.UlceraVenosa.Api;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;
using Cn2x.Iryo.UlceraVenosa.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext registrado anteriormente
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null) {
                    services.Remove(descriptor);
                }

                // Registra o DbContext com a connection string do teste
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(_connectionString);
                });
            });

            //builder.ConfigureAppConfiguration((context, config) =>
            //{
            //    var dict = new Dictionary<string, string?>
            //    {
            //        ["ConnectionStrings:DefaultConnection"] = _connectionString
            //    };
            //    config.AddInMemoryCollection(dict);
            //});
        }
    }
}
