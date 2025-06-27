using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classe patofisiológica da classificação CEAP
/// </summary>
public class ClassePatofisiologica : Entity<Guid>
{
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<ClassificacaoCeap> ClassificacoesCeap { get; set; } = new List<ClassificacaoCeap>();
} 