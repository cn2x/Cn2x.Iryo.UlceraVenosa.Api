using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class ListRegiaoAnatomicaQuery : IRequest<IEnumerable<Domain.Entities.RegiaoAnatomica>>
{
} 