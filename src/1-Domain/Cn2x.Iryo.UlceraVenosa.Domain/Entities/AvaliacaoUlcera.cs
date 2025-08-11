using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class AvaliacaoUlcera : Entity<Guid>, IAggregateRoot
{
    public Guid UlceraId { get; set; }
    public Guid? ProfissionalId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public int MesesDuracao { get; set; } // duração em meses desde o surgimento da úlcera
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    public virtual Medida? Medida { get; set; }
    public virtual ICollection<ImagemAvaliacaoUlcera> Imagens { get; set; } = new List<ImagemAvaliacaoUlcera>();
    public virtual ICollection<ExsudatoDaAvaliacao> Exsudatos { get; set; } = new List<ExsudatoDaAvaliacao>();
    
    public virtual Ulcera? Ulcera { get; set; }
    public virtual Profissional? Profissional { get; set; }
}