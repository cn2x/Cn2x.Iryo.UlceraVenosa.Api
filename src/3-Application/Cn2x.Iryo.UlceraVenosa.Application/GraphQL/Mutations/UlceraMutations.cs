using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Queries;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class UlceraMutations
{
    public async Task<Ulcera?> UpsertUlceraAsync(
        UpsertUlceraInput input,
        [Service] IMediator mediator)
    {
        var command = new UpsertUlceraCommand
        {
            Id = input.Id,
            PacienteId = input.PacienteId,
            Topografias = input.Topografias,
            ClassificacaoCeap = input.ClassificacaoCeap == null ? null :
                new Ceap(
                    Clinica.FromValue<Clinica>(input.ClassificacaoCeap.ClasseClinica),
                    Etiologica.FromValue<Etiologica>(input.ClassificacaoCeap.Etiologia),
                    Anatomica.FromValue<Anatomica>(input.ClassificacaoCeap.Anatomia),
                    Patofisiologica.FromValue<Patofisiologica>(input.ClassificacaoCeap.Patofisiologia)
                )
        };

        var ulceraId = await mediator.Send(command);
        var query = new GetUlceraByIdQuery { Id = ulceraId };
        return await mediator.Send(query);
    }

    public async Task<bool> AtivarInativarUlceraAsync(
        Guid id,
        [Service] IMediator mediator)
    {
        var command = new AtivarInativarUlceraCommand { Id = id };
        return await mediator.Send(command);
    }
}