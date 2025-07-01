using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class ListExsudatoQuery : IRequest<IEnumerable<Domain.Entities.Exsudato>>
{
} 