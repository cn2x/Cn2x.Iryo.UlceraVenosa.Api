using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;

public class ListUlceraQueryHandler : IRequestHandler<ListUlceraQuery, PagedResult<Domain.Entities.Ulcera>>
{
    private readonly IUlceraRepository _ulceraRepository;

    public ListUlceraQueryHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<PagedResult<Domain.Entities.Ulcera>> Handle(ListUlceraQuery request, CancellationToken cancellationToken)
    {
        return await _ulceraRepository.GetPagedAsync(request.Page, request.PageSize, request.SearchTerm);
    }
} 