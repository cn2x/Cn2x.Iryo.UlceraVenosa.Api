using System;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Entidade que representa uma imagem associada a uma AvaliacaoUlcera.
/// </summary>
public class ImagemAvaliacaoUlcera : Entity<Guid>, IAggregateRoot
{
    public Guid AvaliacaoUlceraId { get; set; }
    public Imagem Imagem { get; set; } = null!;

    // Navegação
    public AvaliacaoUlcera AvaliacaoUlcera { get; set; } = null!;
}
