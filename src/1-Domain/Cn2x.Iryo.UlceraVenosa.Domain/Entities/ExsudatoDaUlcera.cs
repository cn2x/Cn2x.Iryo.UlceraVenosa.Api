using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Exsudato da úlcera (tabela de vínculo many-to-many)
/// </summary>
public class ExsudatoDaUlcera : IAggregateRoot
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
    
    // Navegação
    public virtual Ulcera? Ulcera { get; set; }
    public virtual Exsudato? Exsudato { get; set; }
} 