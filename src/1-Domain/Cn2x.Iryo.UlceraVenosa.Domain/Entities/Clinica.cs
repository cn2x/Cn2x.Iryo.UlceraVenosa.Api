using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classe clínica da classificação CEAP
/// </summary>
public class Clinica : Entity<Guid>
{
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<Ceap> ClassificacoesCeap { get; set; } = new List<Ceap>();
} 