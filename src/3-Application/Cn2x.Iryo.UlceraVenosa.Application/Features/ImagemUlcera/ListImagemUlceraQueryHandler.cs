using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class ListImagemUlceraQueryHandler : IRequestHandler<ListImagemUlceraQuery, IEnumerable<Domain.Entities.ImagemUlcera>>
{
    private readonly IRepository<Domain.Entities.ImagemUlcera> _repository;
    public ListImagemUlceraQueryHandler(IRepository<Domain.Entities.ImagemUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.ImagemUlcera>> Handle(ListImagemUlceraQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => !e.Desativada);
    }
} 