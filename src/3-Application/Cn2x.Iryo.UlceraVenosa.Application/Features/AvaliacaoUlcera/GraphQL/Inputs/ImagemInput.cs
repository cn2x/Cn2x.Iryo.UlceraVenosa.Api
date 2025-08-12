using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

public class ImagemInput
{
    public string ArquivoBase64 { get; set; } = string.Empty; // Arquivo em base64
    public string? Descricao { get; set; }
    public DateTime DataCaptura { get; set; }
}
