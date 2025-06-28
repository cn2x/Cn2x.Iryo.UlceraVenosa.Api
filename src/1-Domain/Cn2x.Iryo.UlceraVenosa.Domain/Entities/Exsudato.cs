using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Exsudato da úlcera
/// </summary>
public class Exsudato : Entity<Guid>
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoTipoId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual Ulcera? Ulcera { get; set; }
    public virtual ExsudatoTipo? ExsudatoTipo { get; set; }
} 