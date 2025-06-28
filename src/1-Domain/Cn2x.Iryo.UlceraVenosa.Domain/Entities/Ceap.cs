using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology)
/// </summary>
public class Ceap : Entity<Guid>
{
    public Guid ClasseClinicaId { get; set; }
    public Guid EtiologiaId { get; set; }
    public Guid AnatomiaId { get; set; }
    public Guid PatofisiologiaId { get; set; }
    
    // Navegação
    public virtual Clinica? ClasseClinica { get; set; }
    public virtual Etiologica? Etiologia { get; set; }
    public virtual Anatomica? Anatomia { get; set; }
    public virtual Patofisiologica? Patofisiologia { get; set; }
} 