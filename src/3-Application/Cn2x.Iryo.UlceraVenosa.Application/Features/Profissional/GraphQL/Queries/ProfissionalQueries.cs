using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Queries;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.GraphQL.Queries;

[ExtendObjectType("Query")]
public class ProfissionalQueries
{
    public async Task<Domain.Entities.Profissional?> GetProfissional(
        Guid id,
        [Service] IMediator mediator)
    {
        var query = new GetProfissionalQuery { Id = id };
        return await mediator.Send(query);
    }

    public async Task<IEnumerable<Domain.Entities.Profissional>> GetProfissionais(
        [Service] IMediator mediator)
    {
        var query = new GetAllProfissionaisQuery();
        return await mediator.Send(query);
    }
}

