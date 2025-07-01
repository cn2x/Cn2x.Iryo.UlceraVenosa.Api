using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class ListRegiaoAnatomicaQueryHandler : IRequestHandler<ListRegiaoAnatomicaQuery, IEnumerable<Domain.Entities.RegiaoAnatomica>>
{
    private readonly IRepository<Domain.Entities.RegiaoAnatomica> _repository;
    public ListRegiaoAnatomicaQueryHandler(IRepository<Domain.Entities.RegiaoAnatomica> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.RegiaoAnatomica>> Handle(ListRegiaoAnatomicaQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => !e.Desativada);
    }
} 