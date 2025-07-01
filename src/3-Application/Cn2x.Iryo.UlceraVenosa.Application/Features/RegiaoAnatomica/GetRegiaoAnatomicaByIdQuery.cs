using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class GetRegiaoAnatomicaByIdQuery : IRequest<Domain.Entities.RegiaoAnatomica?>
{
    public Guid Id { get; set; }
    public GetRegiaoAnatomicaByIdQuery(Guid id) => Id = id;
} 