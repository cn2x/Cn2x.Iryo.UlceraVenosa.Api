using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class GetAvaliacaoByIdQuery : IRequest<Domain.Entities.Avaliacao?>
{
    public Guid Id { get; set; }
    public GetAvaliacaoByIdQuery(Guid id) => Id = id;
} 