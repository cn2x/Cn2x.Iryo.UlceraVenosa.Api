using Microsoft.Extensions.DependencyInjection;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

namespace Cn2x.Iryo.UlceraVenosa.Application.Extensions;

/// <summary>
/// Extensões para configuração de serviços da aplicação
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona todos os serviços da aplicação
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registra serviços de upload de arquivo
        services.AddScoped<IFileUploadService, FileUploadService>();
        
        // Registra serviço mock do Google Cloud Storage
        services.AddScoped<IGoogleCloudStorageService, GoogleCloudStorageService>();
        
        return services;
    }
}
