using HotChocolate.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

    /// <summary>
    /// Interface para processar uploads de arquivo (base64)
    /// </summary>
    public interface IFileUploadService
    {
        /// <summary>
        /// Processa um arquivo base64 e retorna os bytes
        /// </summary>
        /// <param name="base64String">String base64 do arquivo</param>
        /// <returns>Bytes do arquivo e metadados</returns>
        Task<FileUploadResult> ProcessBase64Async(string base64String);
        
        /// <summary>
        /// Valida se o arquivo é uma imagem válida
        /// </summary>
        /// <param name="bytes">Bytes do arquivo</param>
        /// <param name="contentType">Tipo de conteúdo</param>
        /// <returns>True se for uma imagem válida</returns>
        bool IsValidImage(byte[] bytes, string contentType);
    }

/// <summary>
/// Resultado do processamento de upload
/// </summary>
public class FileUploadResult
{
    public byte[] Bytes { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string? Description { get; set; }
    public DateTime CaptureDate { get; set; }
}
