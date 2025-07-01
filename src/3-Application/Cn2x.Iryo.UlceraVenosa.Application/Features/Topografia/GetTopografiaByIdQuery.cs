using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class GetTopografiaByIdQuery : IRequest<Domain.Entities.Topografia?>
{
    public Guid Id { get; set; }
    public GetTopografiaByIdQuery(Guid id) => Id = id;
} 