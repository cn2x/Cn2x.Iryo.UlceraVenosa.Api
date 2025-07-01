using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class GetRegiaoAnatomicaByIdQueryHandler : IRequestHandler<GetRegiaoAnatomicaByIdQuery, Domain.Entities.RegiaoAnatomica?>
{
    private readonly IRepository<Domain.Entities.RegiaoAnatomica> _repository;
    public GetRegiaoAnatomicaByIdQueryHandler(IRepository<Domain.Entities.RegiaoAnatomica> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.RegiaoAnatomica?> Handle(GetRegiaoAnatomicaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 