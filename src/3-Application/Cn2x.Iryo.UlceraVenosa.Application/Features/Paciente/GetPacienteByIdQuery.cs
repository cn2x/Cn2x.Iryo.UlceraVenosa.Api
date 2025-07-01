using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class GetPacienteByIdQuery : IRequest<Domain.Entities.Paciente?>
{
    public Guid Id { get; set; }
    public GetPacienteByIdQuery(Guid id) => Id = id;
} 