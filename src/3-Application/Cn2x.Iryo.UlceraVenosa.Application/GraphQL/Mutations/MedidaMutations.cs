using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medidas;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

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
        var id = await mediator.Send(command);
        return new Medida
        {
            Id = id,
            AvaliacaoUlceraId = input.AvaliacaoUlceraId,
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