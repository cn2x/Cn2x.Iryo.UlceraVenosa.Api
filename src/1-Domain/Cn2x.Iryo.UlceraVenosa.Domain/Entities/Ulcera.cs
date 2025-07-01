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
    public Guid AvaliacaoId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public decimal ComprimentoCm { get; set; }
    public decimal Largura { get; set; }
    public decimal Profundidade { get; set; }

    // Value Objects
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();

    // Relacionamentos
    public Ceap ClassificacaoCeap { get; set; } = null!;
    public ICollection<Topografia> Topografias { get; set; } = new List<Topografia>();
    public ICollection<ExsudatoDaUlcera> Exsudatos { get; set; } = new List<ExsudatoDaUlcera>();
    public ICollection<ImagemUlcera> Imagens { get; set; } = new List<ImagemUlcera>();

    // Navegação
    public virtual Avaliacao? Avaliacao { get; set; }
    public virtual Paciente? Paciente { get; set; }
} 