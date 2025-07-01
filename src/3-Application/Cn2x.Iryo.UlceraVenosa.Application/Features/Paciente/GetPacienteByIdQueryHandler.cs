using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class GetPacienteByIdQueryHandler : IRequestHandler<GetPacienteByIdQuery, Domain.Entities.Paciente?>
{
    private readonly IPacienteRepository _repository;
    public GetPacienteByIdQueryHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Paciente?> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 