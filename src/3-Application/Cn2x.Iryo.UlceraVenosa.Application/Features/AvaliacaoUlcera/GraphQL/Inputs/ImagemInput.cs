using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

public class ImagemInput
{
    public string Url { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataCaptura { get; set; }
}
