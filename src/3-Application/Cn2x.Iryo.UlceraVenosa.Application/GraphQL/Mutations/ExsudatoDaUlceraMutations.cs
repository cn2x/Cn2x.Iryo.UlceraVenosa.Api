using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.ExsudatosDaUlcera;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class ExsudatoDaUlceraMutations
{
    public async Task<bool> UpsertExsudatoDaUlceraAsync(
        UpsertExsudatoDaUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertExsudatoDaUlceraCommand
        {
            UlceraId = input.UlceraId,
            ExsudatoId = input.ExsudatoId
        };
        return await mediator.Send(command);
    }
}

public class UpsertExsudatoDaUlceraInput
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
} 