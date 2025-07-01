using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class GetUlceraByIdQueryHandler : IRequestHandler<GetUlceraByIdQuery, Domain.Entities.Ulcera?>
{
    private readonly IUlceraRepository _ulceraRepository;

    public GetUlceraByIdQueryHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<Domain.Entities.Ulcera?> Handle(GetUlceraByIdQuery request, CancellationToken cancellationToken)
    {
        return await _ulceraRepository.GetByIdAsync(request.Id);
    }
} 