using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class UpdatePacienteCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
} 