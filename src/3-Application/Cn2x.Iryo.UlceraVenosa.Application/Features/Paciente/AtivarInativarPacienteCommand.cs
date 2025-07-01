using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class AtivarInativarPacienteCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Desativar { get; set; }
} 