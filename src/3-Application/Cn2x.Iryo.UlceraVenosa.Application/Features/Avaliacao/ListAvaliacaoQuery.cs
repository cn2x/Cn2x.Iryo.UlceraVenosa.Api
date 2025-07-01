using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class ListAvaliacaoQuery : IRequest<IEnumerable<Domain.Entities.Avaliacao>>
{
} 