using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classe clínica da classificação CEAP
/// </summary>
public class ClasseClinica : Entity<Guid>
{
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<ClassificacaoCeap> ClassificacoesCeap { get; set; } = new List<ClassificacaoCeap>();
} 