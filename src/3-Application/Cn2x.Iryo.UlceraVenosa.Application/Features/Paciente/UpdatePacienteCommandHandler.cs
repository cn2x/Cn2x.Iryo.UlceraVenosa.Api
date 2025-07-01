using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class UpdatePacienteCommandHandler : IRequestHandler<UpdatePacienteCommand, bool>
{
    private readonly IPacienteRepository _repository;
    public UpdatePacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdatePacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.Id);
        if (paciente == null) return false;
        paciente.Nome = request.Nome;
        paciente.Cpf = request.Cpf;
        await _repository.UpdateAsync(paciente);
        return true;
    }
} 