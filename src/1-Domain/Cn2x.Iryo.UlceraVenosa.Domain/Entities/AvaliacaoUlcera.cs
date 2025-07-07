using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class AvaliacaoUlcera : Entity<Guid>, IAggregateRoot
{
    public Guid UlceraId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public string Duracao { get; set; } = string.Empty; // desde o surgimento da úlcera
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    public virtual Medida? Medida { get; set; }
    public virtual ICollection<ImagemUlcera> Imagens { get; set; } = new List<ImagemUlcera>();
    public virtual ICollection<ExsudatoDaAvaliacao> Exsudatos { get; set; } = new List<ExsudatoDaAvaliacao>();
    public Ceap ClassificacaoCeap { get; set; } = null!;
    public virtual Ulcera? Ulcera { get; set; }
} 