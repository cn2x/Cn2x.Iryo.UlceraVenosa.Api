namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

/// <summary>
/// Interface para o serviço de Google Cloud Storage
/// </summary>
public interface IGoogleCloudStorageService 
{
    /// <summary>
    /// Faz upload de uma imagem para o Google Cloud Storage
    /// </summary>
    /// <param name="arquivoBase64">Arquivo em formato base64</param>
    /// <param name="avaliacaoUlceraId">ID da avaliação para organizar o bucket</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>URL pública da imagem no Google Cloud Storage</returns>
    Task<string> UploadImagemAsync(string arquivoBase64, Guid avaliacaoUlceraId, CancellationToken cancellationToken = default);
}
