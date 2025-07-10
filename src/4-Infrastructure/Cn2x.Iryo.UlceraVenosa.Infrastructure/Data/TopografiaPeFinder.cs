using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data {
    public class TopografiaPeFinder : ITipografiaFinder
    {
        public async Task<Topografia?> FindAsync(ApplicationDbContext ctx, Guid id, int tipo, CancellationToken cancellationToken, ITipografiaFinder? next = null)
        {
            if (tipo == 2)
            {
                return await ctx.Topografias.OfType<TopografiaPe>()
                    .Include(t => t.Lateralidade)
                    .Include(t => t.RegiaoTopograficaPe)
                    .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            }
            return next != null ? await next.FindAsync(ctx, id, tipo, cancellationToken) : null;
        }
    }
}
