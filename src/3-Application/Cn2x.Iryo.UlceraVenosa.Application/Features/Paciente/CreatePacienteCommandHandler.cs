using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class CreatePacienteCommandHandler : IRequestHandler<CreatePacienteCommand, Guid>
{
    private readonly IPacienteRepository _repository;
    public CreatePacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = new Domain.Entities.Paciente
        {
            Id = request.Id ?? Guid.NewGuid(),
            Nome = request.Nome,
            Cpf = request.Cpf,
            Desativada = false
        };
        await _repository.AddAsync(paciente);
        await _repository.UnitOfWork.SaveChangesAsync();
        return paciente.Id;
    }
} 