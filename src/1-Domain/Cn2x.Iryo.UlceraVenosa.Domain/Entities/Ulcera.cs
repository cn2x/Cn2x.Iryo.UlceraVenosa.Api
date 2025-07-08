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
    // public string Duracao { get; set; } = string.Empty;
    // public DateTime DataExame { get; set; }

    // public Caracteristicas Caracteristicas { get; set; } = new();
    // public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();

    // public Ceap ClassificacaoCeap { get; set; } = null!;
    public ICollection<Topografia> Topografias { get; set; } = new List<Topografia>();
    // public ICollection<ExsudatoDaUlcera> Exsudatos { get; set; } = new List<ExsudatoDaUlcera>();
    // public ICollection<ImagemUlcera> Imagens { get; set; } = new List<ImagemUlcera>();

    // Navegação
    public virtual Paciente? Paciente { get; set; }
    // public virtual Medida? Medida { get; set; }

    // NOVO: Avaliações da úlcera
    public virtual ICollection<AvaliacaoUlcera> Avaliacoes { get; set; } = new List<AvaliacaoUlcera>();
    public Ceap ClassificacaoCeap { get; set; } = null!;
}