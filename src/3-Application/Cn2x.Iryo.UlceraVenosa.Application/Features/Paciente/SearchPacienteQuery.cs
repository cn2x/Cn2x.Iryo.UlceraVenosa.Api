using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;

public class SearchPacienteQuery : IRequest<IEnumerable<Domain.Entities.Paciente>>
{
    public string? Term { get; set; }
} 