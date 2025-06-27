using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology)
/// </summary>
public class ClassificacaoCeap : Entity<Guid>
{
    public Guid ClasseClinicaId { get; set; }
    public Guid EtiologiaId { get; set; }
    public Guid AnatomiaId { get; set; }
    public Guid PatofisiologiaId { get; set; }
    
    // Navegação
    public virtual ClasseClinica? ClasseClinica { get; set; }
    public virtual ClasseEtiologica? Etiologia { get; set; }
    public virtual ClasseAnatomica? Anatomia { get; set; }
    public virtual ClassePatofisiologica? Patofisiologia { get; set; }
} 