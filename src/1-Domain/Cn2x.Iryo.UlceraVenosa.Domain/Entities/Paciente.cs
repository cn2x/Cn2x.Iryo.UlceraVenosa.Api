using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Paciente no contexto de úlceras venosas (dados sincronizados do microserviço de pacientes)
/// </summary>
public class Paciente : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    // Navegação
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    public virtual ICollection<Ulcera> Ulceras { get; set; } = new List<Ulcera>();
} 