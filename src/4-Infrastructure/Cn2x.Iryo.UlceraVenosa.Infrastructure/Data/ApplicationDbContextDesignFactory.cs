using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Lê o appsettings.json da Presentation
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "2-Presentation", "Cn2x.Iryo.UlceraVenosa.Api");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            // Mock simples de IHttpContextAccessor
            var httpContextAccessor = new HttpContextAccessor();
            return new ApplicationDbContext(optionsBuilder.Options, new NoMediator(), httpContextAccessor) { IsDesign = true };
        }
    }
} 