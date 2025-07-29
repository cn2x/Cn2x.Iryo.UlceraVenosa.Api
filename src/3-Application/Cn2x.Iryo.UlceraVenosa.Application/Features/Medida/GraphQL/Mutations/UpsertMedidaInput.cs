namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Mutations;

public class UpsertMedidaInput
{
    public Guid AvaliacaoUlceraId { get; set; }
    public decimal? Comprimento { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Profundidade { get; set; }
} 