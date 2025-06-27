using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Tipo de exsudato
/// </summary>
public class ExsudatoTipo : Entity<Guid>
{
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<Exsudato> Exsudatos { get; set; } = new List<Exsudato>();
} 