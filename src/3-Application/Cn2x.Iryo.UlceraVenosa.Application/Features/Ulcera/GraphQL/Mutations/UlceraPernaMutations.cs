using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Perna;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraPernaMutations
{
    public async Task<Domain.Entities.Ulcera?> UpsertUlceraPernaAsync(
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
        
        return null; 
    }
} 