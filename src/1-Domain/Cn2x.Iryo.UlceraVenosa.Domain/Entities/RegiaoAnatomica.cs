using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Região anatômica
/// </summary>
public class RegiaoAnatomica : Entity<Guid>
{
    public Guid SegmentoId { get; set; }
    public string Limites { get; set; } = string.Empty;
    
    // Navegação
    public virtual Segmento? Segmento { get; set; }
    public virtual ICollection<Topografia> Topografias { get; set; } = new List<Topografia>();
} 