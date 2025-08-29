using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Queries;

[ExtendObjectType("Query")]
public class AvaliacaoUlceraQueries
{
    [GraphQLName("avaliacoesByPaciente")]
    public async Task<IEnumerable<Domain.Entities.AvaliacaoUlcera>> GetAvaliacoesByPacienteAsync(
        Guid pacienteId,
        [Service] ApplicationDbContext context)
    {
        return await context.GetAvaliacoesByPacienteAsync(pacienteId);
    }
}
