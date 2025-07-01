using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class GetImagemUlceraByIdQueryHandler : IRequestHandler<GetImagemUlceraByIdQuery, Domain.Entities.ImagemUlcera?>
{
    private readonly IRepository<Domain.Entities.ImagemUlcera> _repository;
    public GetImagemUlceraByIdQueryHandler(IRepository<Domain.Entities.ImagemUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.ImagemUlcera?> Handle(GetImagemUlceraByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 