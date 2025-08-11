using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Queries;

public class GetAllProfissionaisQuery : IRequest<IEnumerable<Domain.Entities.Profissional>>
{
}

