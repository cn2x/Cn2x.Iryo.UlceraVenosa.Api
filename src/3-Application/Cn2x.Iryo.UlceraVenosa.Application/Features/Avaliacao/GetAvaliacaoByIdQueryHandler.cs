using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class GetAvaliacaoByIdQueryHandler : IRequestHandler<GetAvaliacaoByIdQuery, Domain.Entities.Avaliacao?>
{
    private readonly IRepository<Domain.Entities.Avaliacao> _repository;
    public GetAvaliacaoByIdQueryHandler(IRepository<Domain.Entities.Avaliacao> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Avaliacao?> Handle(GetAvaliacaoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 