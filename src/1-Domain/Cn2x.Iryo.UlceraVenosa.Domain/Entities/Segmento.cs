using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Segmento anatômico (persistido em tabela)
/// </summary>
public class Segmento : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public virtual ICollection<Ulcera> Ulceras { get; set; } = new List<Ulcera>();
}
