using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Topografia da úlcera
/// </summary>
public class Topografia : Entity<Guid>
{
    public Guid UlceraId { get; set; }
    public Guid RegiaoId { get; set; }
    public string Lado { get; set; } = string.Empty; // Ex: direito, esquerdo, bilateral
    
    // Navegação
    public virtual Ulcera? Ulcera { get; set; }
    public virtual RegiaoAnatomica? Regiao { get; set; }
} 