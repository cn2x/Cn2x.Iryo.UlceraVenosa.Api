using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class ListImagemUlceraQuery : IRequest<IEnumerable<Domain.Entities.ImagemUlcera>>
{
} 