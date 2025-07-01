using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class GetImagemUlceraByIdQuery : IRequest<Domain.Entities.ImagemUlcera?>
{
    public Guid Id { get; set; }
    public GetImagemUlceraByIdQuery(Guid id) => Id = id;
} 