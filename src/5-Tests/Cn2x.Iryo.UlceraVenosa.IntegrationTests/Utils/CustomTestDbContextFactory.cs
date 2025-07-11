using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils
{
    public class CustomTestDbContextFactory
    {
        public static ApplicationDbContext CreateAndSeed(string connectionString)
        {
            var services = new ServiceCollection();
            var mediatorMock = new Mock<IMediator>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            services.AddSingleton(mediatorMock.Object);
            services.AddSingleton(httpContextAccessorMock.Object);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            var sp = services.BuildServiceProvider();

            
            using var scope = sp.CreateScope();           
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
            TestSeedData.Seed(db);
            return db;
        }
    }
}