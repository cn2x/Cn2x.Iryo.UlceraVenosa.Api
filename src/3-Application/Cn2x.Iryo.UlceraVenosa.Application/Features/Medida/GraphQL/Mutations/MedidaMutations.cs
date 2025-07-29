using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medida;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class MedidaMutations
{
    public async Task<Domain.ValueObjects.Medida?> UpsertMedidaAsync(
        UpsertMedidaInput input,
        [Service] IMediator mediator)
    {
        var command = new Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.UpsertMedidaCommand
        {
            AvaliacaoUlceraId = input.AvaliacaoUlceraId,
            Comprimento = input.Comprimento,
            Largura = input.Largura,
            Profundidade = input.Profundidade
        };
        var avaliacaoId = await mediator.Send(command);
        // Buscar a AvaliacaoUlcera para retornar a Medida atualizada
        // (supondo que existe um método para buscar por id)
        // Aqui, para simplificar, retornamos o input como Medida
        return new Domain.ValueObjects.Medida
        {
            Comprimento = input.Comprimento,
            Largura = input.Largura,
            Profundidade = input.Profundidade
        };
    }
}
