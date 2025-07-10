using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

[ExtendObjectType("Query")]
public class TopografiaQueries
{
    [GraphQLName("topografiasPerna")]
    public async Task<List<TopografiaPerna>> GetTopografiasPerna([Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPerna>()
            .Include(t => t.Lateralidade)
            .Include(t => t.Segmentacao)
            .Include(t => t.RegiaoAnatomica)
            .ToListAsync();
    }

    [GraphQLName("topografiasPe")]
    public async Task<List<TopografiaPe>> GetTopografiasPe([Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPe>()
            .Include(t => t.Lateralidade)
            .Include(t => t.RegiaoTopograficaPe)
            .ToListAsync();
    }

    [GraphQLName("topografiaPernaPorId")]
    public async Task<TopografiaPerna?> GetTopografiaPernaPorId(int id, [Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPerna>()
            .Include(t => t.Lateralidade)
            .Include(t => t.Segmentacao)
            .Include(t => t.RegiaoAnatomica)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    [GraphQLName("topografiaPePorId")]
    public async Task<TopografiaPe?> GetTopografiaPePorId(int id, [Service] ApplicationDbContext ctx)
    {
        return await ctx.Set<TopografiaPe>()
            .Include(t => t.Lateralidade)
            .Include(t => t.RegiaoTopograficaPe)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
