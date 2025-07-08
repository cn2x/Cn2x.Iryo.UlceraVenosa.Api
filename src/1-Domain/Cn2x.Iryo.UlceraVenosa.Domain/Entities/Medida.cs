using System;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class Medida : IAggregateRoot
{
    public Guid Id { get; set; }
    public Guid AvaliacaoUlceraId { get; set; }
    public decimal? Comprimento { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Profundidade { get; set; }

    public virtual AvaliacaoUlcera AvaliacaoUlcera { get; set; } = null!;
}