using Microsoft.Extensions.Logging;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Services;

/// <summary>
/// Implementação do serviço de upload de arquivo
/// </summary>
public class FileUploadService : IFileUploadService
{
    private readonly ILogger<FileUploadService> _logger;

    public FileUploadService(ILogger<FileUploadService> logger)
    {
        _logger = logger;
    }

    public Task<FileUploadResult> ProcessBase64Async(string base64String)
    {
        try
        {
            // Remover o header data:image/...;base64, se existir
            var base64Data = base64String;
            if (base64String.Contains(","))
            {
                base64Data = base64String.Split(',')[1];
            }

            var bytes = Convert.FromBase64String(base64Data);
            
            // Detectar tipo de conteúdo e nome do arquivo
            var contentType = DetectContentType(bytes);
            var fileName = $"image_{DateTime.UtcNow:yyyyMMddHHmmss}.{GetFileExtension(contentType)}";

            var result = new FileUploadResult
            {
                Bytes = bytes,
                ContentType = contentType,
                FileName = fileName,
                SizeBytes = bytes.Length,
                Description = "Imagem da úlcera",
                CaptureDate = DateTime.UtcNow
            };
            
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar arquivo base64");
            throw new ArgumentException("Arquivo base64 inválido", ex);
        }
    }



    public bool IsValidImage(byte[] bytes, string contentType)
    {
        try
        {
            // Verificar se é um formato suportado
            var supportedFormats = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp" };
            if (!supportedFormats.Contains(contentType.ToLower()))
                return false;
            
            // Verificar se tem tamanho mínimo (pelo menos alguns bytes)
            if (bytes.Length < 10)
                return false;
            
            // Verificar bytes mágicos para formatos comuns
            if (contentType.ToLower().Contains("jpeg") || contentType.ToLower().Contains("jpg"))
            {
                if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xD8)
                    return true;
            }
            else if (contentType.ToLower().Contains("png"))
            {
                if (bytes.Length >= 8 && bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
                    return true;
            }
            else if (contentType.ToLower().Contains("gif"))
            {
                if (bytes.Length >= 6 && bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46)
                    return true;
            }
            else if (contentType.ToLower().Contains("bmp"))
            {
                if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
                    return true;
            }
            
            return false;
        }
        catch
        {
            return false;
        }
    }

    private string DetectContentType(byte[] bytes)
    {
        if (bytes.Length < 2) return "application/octet-stream";
        
        // JPEG
        if (bytes[0] == 0xFF && bytes[1] == 0xD8)
            return "image/jpeg";
        
        // PNG
        if (bytes.Length >= 8 && bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return "image/png";
        
        // GIF
        if (bytes.Length >= 6 && bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46)
            return "image/gif";
        
        // BMP
        if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
            return "image/bmp";
        
        return "application/octet-stream";
    }

    private string GetFileExtension(string contentType)
    {
        return contentType.ToLower() switch
        {
            "image/jpeg" or "image/jpg" => "jpg",
            "image/png" => "png",
            "image/gif" => "gif",
            "image/bmp" => "bmp",
            _ => "bin"
        };
    }
}
