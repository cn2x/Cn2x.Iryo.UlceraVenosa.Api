using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Events;

/// <summary>
/// Evento de domínio disparado quando uma imagem precisa ser enviada para o Google Cloud Storage
/// </summary>
public class ImagemUploadSolicitadaEvent : INotification
{
    public Guid AvaliacaoUlceraId { get; }
    public string ArquivoBase64 { get; }
    public string? Descricao { get; }
    public DateTime DataCaptura { get; }
    public DateTime OcorridoEm { get; }

    public ImagemUploadSolicitadaEvent(
        Guid avaliacaoUlceraId, 
        string arquivoBase64, 
        string? descricao, 
        DateTime dataCaptura)
    {
        AvaliacaoUlceraId = avaliacaoUlceraId;
        ArquivoBase64 = arquivoBase64;
        Descricao = descricao;
        DataCaptura = dataCaptura;
        OcorridoEm = DateTime.UtcNow;
    }
}
