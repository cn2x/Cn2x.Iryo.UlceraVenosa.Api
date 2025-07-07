using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;

public class ListUlceraQuery : IRequest<PagedResult<Domain.Entities.Ulcera>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
} 