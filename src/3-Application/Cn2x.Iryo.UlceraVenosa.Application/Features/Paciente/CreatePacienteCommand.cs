using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class CreatePacienteCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
} 