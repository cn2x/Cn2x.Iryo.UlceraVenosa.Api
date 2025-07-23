using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class AvaliacaoUlceraMutations
{
    public async Task<AvaliacaoUlcera?> UpsertAvaliacaoUlceraAsync(
        UpsertAvaliacaoUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertAvaliacaoUlceraCommand
        {
            Id = input.Id,
            UlceraId = input.UlceraId,
            DataAvaliacao = input.DataAvaliacao,
            MesesDuracao = input.MesesDuracao,
            Caracteristicas = input.Caracteristicas,
            SinaisInflamatorios = input.SinaisInflamatorios,
            Medida = input.Medida,
            Imagens = input.Imagens,
            Exsudatos = input.Exsudatos
        };
        var avaliacaoId = await mediator.Send(command);        
        return null; // ou retorne a consulta se implementada
    }
}
