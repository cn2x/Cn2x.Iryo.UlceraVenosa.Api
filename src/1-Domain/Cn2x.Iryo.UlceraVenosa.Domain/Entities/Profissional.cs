using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Profissional de saúde que realiza avaliações de úlceras
/// </summary>
public class Profissional : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; set; } = string.Empty;

    // Navegação
    public virtual ICollection<AvaliacaoUlcera> Avaliacoes { get; set; } = new List<AvaliacaoUlcera>();
}

