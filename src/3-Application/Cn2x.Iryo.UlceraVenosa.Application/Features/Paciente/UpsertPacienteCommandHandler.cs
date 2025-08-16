using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class UpsertPacienteCommandHandler(IPacienteRepository repository)
    : IRequestHandler<UpsertPacienteCommandRequest, bool>
{
    private readonly IPacienteRepository _repository = repository;

    public async Task<bool> Handle(UpsertPacienteCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Paciente newPaciente = new()
        {
            Cpf = request.Cpf,
            Nome = request.Nome
        };

        var paciente = await _repository.GetWithCpfAsync(request.Cpf);

        if (paciente == null)
        {
            await _repository.SaveAsync(newPaciente);
        }
        else
        {
            await _repository.Update(paciente);
        }   
        return await Task.FromResult(true);
    }
}