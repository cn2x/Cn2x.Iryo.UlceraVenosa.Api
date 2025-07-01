using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Segmento anatômico
/// </summary>
public class Segmento : Entity<Guid>, IAggregateRoot
{
    public string Descricao { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<RegiaoAnatomica> RegioesAnatomicas { get; set; } = new List<RegiaoAnatomica>();
} 