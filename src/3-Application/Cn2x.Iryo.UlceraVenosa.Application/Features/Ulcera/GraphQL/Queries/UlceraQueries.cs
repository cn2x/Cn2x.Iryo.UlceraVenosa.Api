using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Queries;

[ExtendObjectType("Query")]
public class UlceraQueries
{
    [GraphQLName("ulcera")]
    public async Task<Domain.Entities.Ulcera?> GetUlceraAsync(
        Guid id,
        [Service] ApplicationDbContext context)
    {
        return await context.GetUlceraAsync(id);
    }

    [GraphQLName("ulceras")]
    public async Task<PagedResult<Domain.Entities.Ulcera>> GetUlcerasAsync(
        int page = 1,
        int pageSize = 10,
        string? searchTerm = null,
        [Service] ApplicationDbContext context = null!)
    {
        return await context.GetUlcerasAsync(page, pageSize, searchTerm);
    }

    [GraphQLName("ulcerasByPaciente")]
    public async Task<IEnumerable<Domain.Entities.Ulcera>> GetUlcerasByPacienteAsync(
        Guid pacienteId,
        [Service] ApplicationDbContext context)
    {
        return await context.GetUlcerasByPacienteAsync(pacienteId);
    }
} 