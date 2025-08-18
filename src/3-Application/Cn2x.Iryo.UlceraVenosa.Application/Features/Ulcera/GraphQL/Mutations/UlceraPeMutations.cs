using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Pe;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraPeMutations
{
    public async Task<Domain.Entities.Ulcera?> UpsertUlceraPeAsync(
        UpsertUlceraPeInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertUlceraPeCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            LateralidadeId = input.Topografia.LateralidadeId,
            RegiaoAnatomicaId = input.Topografia.RegiaoAnatomicaId,
            RegiaoTopograficaPeId = input.Topografia.RegiaoTopograficaPeId,
            ClassificacaoCeap = input.ClassificacaoCeap
        };

        return await mediator.Send(command);
    }
} 