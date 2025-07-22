using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Perna;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraPernaMutations
{
    public async Task<Ulcera?> UpsertUlceraPernaAsync(
        UpsertUlceraPernaInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertUlceraPernaCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            LateralidadeId = input.Topografia.LateralidadeId,
            SegmentacaoId = input.Topografia.SegmentacaoId,
            RegiaoAnatomicaId = input.Topografia.RegiaoAnatomicaId,
            ClassificacaoCeap = input.ClassificacaoCeap
        };

        var ulceraId = await mediator.Send(command);
        var query = new GetUlceraByIdQuery { Id = ulceraId };
        return await mediator.Send(query);
    }
}
