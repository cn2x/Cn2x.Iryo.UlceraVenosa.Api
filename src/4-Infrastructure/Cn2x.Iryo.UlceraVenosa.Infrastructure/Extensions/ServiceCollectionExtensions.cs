using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;

/// <summary>
/// Extensões para configuração de serviços
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona todos os serviços da infraestrutura
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Registra repositórios usando Scrutor/assembly scan
        services.Scan(scan => scan
            .FromAssemblyOf<UlceraRepository>()
            .AddClasses(c => c.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        // Registro explícito do repositório genérico
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        
        return services;
    }
} 