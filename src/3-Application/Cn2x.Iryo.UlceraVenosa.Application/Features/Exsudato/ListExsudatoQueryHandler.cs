using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using System.Linq;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class ListExsudatoQueryHandler : IRequestHandler<ListExsudatoQuery, IEnumerable<Domain.Entities.Exsudato>>
{
    private readonly IRepository<Domain.Entities.Exsudato> _repository;
    public ListExsudatoQueryHandler(IRepository<Domain.Entities.Exsudato> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.Entities.Exsudato>> Handle(ListExsudatoQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => !e.Desativada);
    }
} 