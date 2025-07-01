using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class GetTopografiaByIdQueryHandler : IRequestHandler<GetTopografiaByIdQuery, Domain.Entities.Topografia?>
{
    private readonly IRepository<Domain.Entities.Topografia> _repository;
    public GetTopografiaByIdQueryHandler(IRepository<Domain.Entities.Topografia> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Topografia?> Handle(GetTopografiaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 