using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classe patofisiológica da classificação CEAP
/// </summary>
public class Patofisiologica : Entity<Guid>
{
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<Ceap> ClassificacoesCeap { get; set; } = new List<Ceap>();
} 