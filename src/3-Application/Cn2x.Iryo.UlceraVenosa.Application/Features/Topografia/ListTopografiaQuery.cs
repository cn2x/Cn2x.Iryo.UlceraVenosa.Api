using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class ListTopografiaQuery : IRequest<IEnumerable<Domain.Entities.Topografia>>
{
} 