using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Queries;

[ExtendObjectType("Query")]
public class TopografiaQueries
{
    [GraphQLName("topografiasPerna")]
    public async Task<List<TopografiaPerna>> GetTopografiasPerna([Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPerna>()
            .AsNoTracking()
            .Include(t => t.Lateralidade)
            .Include(t => t.Segmentacao)
            .Include(t => t.RegiaoAnatomica)
            .ToListAsync();
    }

    [GraphQLName("topografiasPe")]
    public async Task<List<TopografiaPe>> GetTopografiasPe([Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPe>()
            .AsNoTracking()
            .Include(t => t.Lateralidade)
            .Include(t => t.RegiaoTopograficaPe)
            .ToListAsync();
    }

    [GraphQLName("topografiaPernaPorId")]
    public async Task<TopografiaPerna?> GetTopografiaPernaPorId(Guid id, [Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPerna>()
            .AsNoTracking()
            .Include(t => t.Lateralidade)
            .Include(t => t.Segmentacao)
            .Include(t => t.RegiaoAnatomica)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    [GraphQLName("topografiaPePorId")]
    public async Task<TopografiaPe?> GetTopografiaPePorId(Guid id, [Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPe>()
            .AsNoTracking()
            .Include(t => t.Lateralidade)
            .Include(t => t.RegiaoTopograficaPe)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
} 