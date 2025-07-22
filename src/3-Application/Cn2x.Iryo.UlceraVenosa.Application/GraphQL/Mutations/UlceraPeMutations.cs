using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Pe;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraPeMutations
{
    public async Task<Ulcera?> UpsertUlceraPeAsync(
        UpsertUlceraPeInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertUlceraPeCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            LateralidadeId = input.Topografia.LateralidadeId,
            RegiaoAnatomicaId = input.Topografia.RegiaoAnatomicaId,
            ClassificacaoCeap = input.ClassificacaoCeap
        };

        var ulceraId = await mediator.Send(command);
        var query = new GetUlceraByIdQuery { Id = ulceraId };
        return await mediator.Send(query);
    }
}
