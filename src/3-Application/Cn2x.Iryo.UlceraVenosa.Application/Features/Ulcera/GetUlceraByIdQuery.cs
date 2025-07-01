using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class GetUlceraByIdQuery : IRequest<Domain.Entities.Ulcera?>
{
    public Guid Id { get; set; }
} 