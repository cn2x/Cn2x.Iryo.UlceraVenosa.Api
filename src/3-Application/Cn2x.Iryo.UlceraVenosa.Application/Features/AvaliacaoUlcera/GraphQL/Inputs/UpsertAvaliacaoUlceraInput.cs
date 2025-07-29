using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

public class UpsertAvaliacaoUlceraInput
{
    public Guid? Id { get; set; }
    public Guid UlceraId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public int MesesDuracao { get; set; }    
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    public Domain.ValueObjects.Medida? Medida { get; set; }
    public List<Guid>? Imagens { get; set; }
    public List<Guid>? Exsudatos { get; set; }
} 