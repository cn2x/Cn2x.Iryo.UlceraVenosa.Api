using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Avaliação do paciente
/// </summary>
public class Avaliacao : Entity<Guid>
{
    public Guid PacienteId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public string Diagnostico { get; set; } = string.Empty;
    public string Conduta { get; set; } = string.Empty;

    // Navegação
    public virtual Paciente? Paciente { get; set; }
    public virtual ICollection<Ulcera> Ulceras { get; set; } = new List<Ulcera>();
} 