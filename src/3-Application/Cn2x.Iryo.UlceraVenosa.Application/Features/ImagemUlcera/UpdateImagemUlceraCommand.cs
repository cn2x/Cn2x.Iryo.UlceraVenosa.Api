using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class UpdateImagemUlceraCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UlceraId { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long TamanhoBytes { get; set; }
    public DateTime DataCaptura { get; set; }
    public string? Descricao { get; set; }
    public string? Observacoes { get; set; }
    public bool EhImagemPrincipal { get; set; } = false;
    public int OrdemExibicao { get; set; } = 0;
} 