using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;
using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class PacienteMutations
{
    public async Task<bool> UpsertPacienteAsync(
        string Cpf, 
        [Service] IMediator mediator)
    {
        var command = new UpsertPacienteCommandRequest {Cpf = Cpf};
        return await mediator.Send(command);
    }
}