using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class ListAvaliacaoQueryHandler : IRequestHandler<ListAvaliacaoQuery, IEnumerable<Domain.Entities.Avaliacao>>
{
    private readonly IRepository<Domain.Entities.Avaliacao> _repository;
    public ListAvaliacaoQueryHandler(IRepository<Domain.Entities.Avaliacao> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.Avaliacao>> Handle(ListAvaliacaoQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(a => !a.Desativada);
    }
} 