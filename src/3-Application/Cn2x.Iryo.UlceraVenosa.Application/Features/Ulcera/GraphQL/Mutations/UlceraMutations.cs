using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraMutations
{
    public async Task<bool> AtivarInativarUlceraAsync(
        Guid id,
        [Service] IMediator mediator)
    {
        var command = new AtivarInativarUlceraCommand { Id = id };
        return await mediator.Send(command);
    }
} 