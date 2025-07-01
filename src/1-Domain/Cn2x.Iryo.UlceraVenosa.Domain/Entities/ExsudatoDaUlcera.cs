using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Exsudato da úlcera (agora ExsudatoDaUlcera)
/// </summary>
public class ExsudatoDaUlcera : Entity<Guid>
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    // Navegação
    public virtual Ulcera? Ulcera { get; set; }
    public virtual Exsudato? Exsudato { get; set; }
} 