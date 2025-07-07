using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class UlceraQueries
{
    [GraphQLName("ulcera")]
    public async Task<Ulcera?> GetUlceraAsync(
        Guid id,
        [Service] IMediator mediator)
    {
        var query = new GetUlceraByIdQuery { Id = id };
        return await mediator.Send(query);
    }

    [GraphQLName("ulceras")]
    public async Task<PagedResult<Ulcera>> GetUlcerasAsync(
        int page = 1,
        int pageSize = 10,
        string? searchTerm = null,
        [Service] IMediator mediator = null!)
    {
        var query = new ListUlceraQuery 
        { 
            Page = page, 
            PageSize = pageSize, 
            SearchTerm = searchTerm 
        };
        
        return await mediator.Send(query);
    }
} 