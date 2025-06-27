using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

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
        // Registra repositórios usando assembly scanning
        services.AddRepositories();
        
        return services;
    }

    /// <summary>
    /// Registra repositórios automaticamente usando assembly scanning
    /// </summary>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        // Encontra todas as interfaces de repositório
        var repositoryInterfaces = assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository") && t != typeof(IBaseRepository<>))
            .ToList();

        // Encontra todas as implementações de repositório
        var repositoryImplementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
            .ToList();

        // Registra cada implementação com sua interface correspondente
        foreach (var implementation in repositoryImplementations)
        {
            var interfaceType = repositoryInterfaces
                .FirstOrDefault(i => implementation.GetInterfaces().Contains(i));

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementation);
            }
        }

        // Registra repositórios específicos que podem não ser encontrados pelo scanning
        services.AddScoped<IUlceraRepository, UlceraRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();

        return services;
    }
} 