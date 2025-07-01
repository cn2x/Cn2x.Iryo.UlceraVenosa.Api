using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class AtivarInativarPacienteCommandHandler : IRequestHandler<AtivarInativarPacienteCommand, bool>
{
    private readonly IPacienteRepository _repository;
    public AtivarInativarPacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarPacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.Id);
        if (paciente == null) return false;
        paciente.Desativada = request.Desativar;
        await _repository.UpdateAsync(paciente);
        return true;
    }
} 