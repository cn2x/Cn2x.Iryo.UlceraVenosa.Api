using Microsoft.Extensions.Logging;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

/// <summary>
/// Implementação mock do serviço de Google Cloud Storage para desenvolvimento
/// </summary>
public class GoogleCloudStorageService : IGoogleCloudStorageService
{
    private readonly ILogger<GoogleCloudStorageService> _logger;

    public GoogleCloudStorageService(ILogger<GoogleCloudStorageService> logger)
    {
        _logger = logger;
    }

    public async Task<string> UploadImagemAsync(string arquivoBase64, Guid avaliacaoUlceraId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock: Upload de imagem para avaliação {AvaliacaoId}", avaliacaoUlceraId);
        
        // Simular delay de upload
        await Task.Delay(100, cancellationToken);
        
        // Retornar URL mock
        var url = $"https://storage.googleapis.com/mock-bucket/ulceras/{avaliacaoUlceraId}/imagem_{DateTime.UtcNow:yyyyMMddHHmmss}.jpg";
        
        _logger.LogInformation("Mock: Imagem enviada com sucesso. URL: {Url}", url);
        
        return url;
    }
}
