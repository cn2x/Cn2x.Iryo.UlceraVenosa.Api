using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Queries;

public class GetAllProfissionaisQueryHandler : IRequestHandler<GetAllProfissionaisQuery, IEnumerable<Domain.Entities.Profissional>>
{
    private readonly IProfissionalRepository _profissionalRepository;

    public GetAllProfissionaisQueryHandler(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Profissional>> Handle(GetAllProfissionaisQuery request, CancellationToken cancellationToken)
    {
        return await _profissionalRepository.GetAllAsync();
    }
}

