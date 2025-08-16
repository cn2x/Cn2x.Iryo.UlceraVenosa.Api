using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Paciente no contexto de úlceras venosas (dados sincronizados do microserviço de pacientes)
/// </summary>
public class Paciente : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    
    public ICollection<Contato> Contatos { get; set; } = new List<Contato>();

    // Navegação
    public virtual ICollection<Ulcera> Ulceras { get; set; } = new List<Ulcera>();
} 