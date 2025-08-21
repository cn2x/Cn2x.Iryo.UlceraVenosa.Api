using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Commands;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class ProfissionalMutations
{
    public async Task<Guid> UpsertProfissional(
        Guid? id,
        string nome,
        [Service] IMediator mediator)
    {
        var command = new UpsertProfissionalCommand
        {
            Id = id,
            Nome = nome
        };
        
        return await mediator.Send(command);
    }

    public async Task<bool> DeleteProfissional(
        Guid id,
        [Service] IMediator mediator)
    {
        var command = new DeleteProfissionalCommand { Id = id };
        return await mediator.Send(command);
    }
}















