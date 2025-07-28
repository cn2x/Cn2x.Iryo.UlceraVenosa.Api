using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medidas;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class MedidaMutations
{
    public async Task<Medida?> UpsertMedidaAsync(
        UpsertMedidaInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertMedidaCommand
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
        return new Medida
        {
            Comprimento = input.Comprimento,
            Largura = input.Largura,
            Profundidade = input.Profundidade
        };
    }
}

public class UpsertMedidaInput
{
    public Guid AvaliacaoUlceraId { get; set; }
    public decimal? Comprimento { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Profundidade { get; set; }
}