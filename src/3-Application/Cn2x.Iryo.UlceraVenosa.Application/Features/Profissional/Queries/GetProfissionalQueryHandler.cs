using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Queries;

public class GetProfissionalQueryHandler : IRequestHandler<GetProfissionalQuery, Domain.Entities.Profissional?>
{
    private readonly IProfissionalRepository _profissionalRepository;

    public GetProfissionalQueryHandler(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task<Domain.Entities.Profissional?> Handle(GetProfissionalQuery request, CancellationToken cancellationToken)
    {
        return await _profissionalRepository.GetByIdAsync(request.Id);
    }
}

