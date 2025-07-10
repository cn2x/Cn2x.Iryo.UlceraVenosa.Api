using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Entidade principal para representar uma úlcera venosa
/// </summary>
public class Ulcera : Entity<Guid>, IAggregateRoot
{
    public Guid PacienteId { get; set; }    
    public virtual Paciente? Paciente { get; set; }
    public virtual ICollection<AvaliacaoUlcera> Avaliacoes { get; set; } = new List<AvaliacaoUlcera>();
    public Ceap Ceap { get; set; } = null!;

    public Guid TopografiaId { get; set; }
    public required Topografia Topografia { get; set; }
}