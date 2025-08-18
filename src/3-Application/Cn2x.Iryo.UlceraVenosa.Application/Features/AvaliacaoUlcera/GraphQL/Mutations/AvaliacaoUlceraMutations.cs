using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class AvaliacaoUlceraMutations
{
    public async Task<Domain.Entities.AvaliacaoUlcera?> UpsertAvaliacaoUlceraAsync(
        UpsertAvaliacaoUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertAvaliacaoUlceraCommand
        {
            Id = input.Id,
            UlceraId = input.UlceraId,
            ProfissionalId = input.ProfissionalId,
            DataAvaliacao = input.DataAvaliacao,
            MesesDuracao = input.MesesDuracao,
            Caracteristicas = input.Caracteristicas,
            SinaisInflamatorios = input.SinaisInflamatorios,
            Medida = input.Medida,
            Exsudatos = input.Exsudatos,
            Arquivo = input.Arquivo,
            DescricaoImagem = input.DescricaoImagem,
            DataCapturaImagem = input.DataCapturaImagem
        };

        return await mediator.Send(command);
    }
} 