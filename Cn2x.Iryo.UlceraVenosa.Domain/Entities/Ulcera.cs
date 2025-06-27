using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

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
    public decimal LarguraCm { get; set; }
    public decimal ProfundidadeCm { get; set; }

    // Value Objects
    public UlceraCaracteristicas Caracteristicas { get; set; } = new();
    public UlceraSinaisInflamatorios SinaisInflamatorios { get; set; } = new();

    // Relacionamentos
    public ClassificacaoCeap ClassificacaoCeap { get; set; } = new();
    public ICollection<Topografia> Topografias { get; set; } = new List<Topografia>();
    public ICollection<Exsudato> Exsudatos { get; set; } = new List<Exsudato>();

    // Navegação
    public virtual Avaliacao? Avaliacao { get; set; }
    public virtual Paciente? Paciente { get; set; }
    public virtual ICollection<Topografia> TopografiasNavigation { get; set; } = new List<Topografia>();
    public virtual ICollection<Exsudato> ExsudatosNavigation { get; set; } = new List<Exsudato>();
} 