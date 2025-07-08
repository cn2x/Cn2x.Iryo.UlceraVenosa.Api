using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Entidade principal para representar uma úlcera venosa
/// </summary>
public class Ulcera : Entity<Guid>, IAggregateRoot
{
    public Guid PacienteId { get; set; }
    public ICollection<Topografia> Topografias { get; set; } = new List<Topografia>();
    
    public virtual Paciente? Paciente { get; set; }
    
    public virtual ICollection<AvaliacaoUlcera> Avaliacoes { get; set; } = new List<AvaliacaoUlcera>();
    public Ceap ClassificacaoCeap { get; set; } = null!;
}