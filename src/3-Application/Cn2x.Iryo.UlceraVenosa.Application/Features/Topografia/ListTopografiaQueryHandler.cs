using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class ListTopografiaQueryHandler : IRequestHandler<ListTopografiaQuery, IEnumerable<Domain.Entities.Topografia>>
{
    private readonly IRepository<Domain.Entities.Topografia> _repository;
    public ListTopografiaQueryHandler(IRepository<Domain.Entities.Topografia> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.Topografia>> Handle(ListTopografiaQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => !e.Desativada);
    }
} 