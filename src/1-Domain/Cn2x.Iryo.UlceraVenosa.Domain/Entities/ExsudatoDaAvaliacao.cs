using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Exsudato da avaliação (tabela de vínculo many-to-many)
/// </summary>
public class ExsudatoDaAvaliacao : IAggregateRoot
{
    public Guid AvaliacaoUlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
    
    // Navegação
    public virtual AvaliacaoUlcera? AvaliacaoUlcera { get; set; }
    public virtual Exsudato? Exsudato { get; set; }
} 