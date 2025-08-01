namespace Cn2x.Paciente.Application.Features.Paciente.Messages;

public class PacienteUpdatedMessage {
    public Guid PacienteId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string EventType { get; set; } = "PacienteUpdated";
}
