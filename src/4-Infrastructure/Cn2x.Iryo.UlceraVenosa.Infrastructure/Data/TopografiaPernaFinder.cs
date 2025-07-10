using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data {
    public class TopografiaPernaFinder : ITipografiaFinder
    {
        public async Task<Topografia?> FindAsync(ApplicationDbContext ctx, int id, int tipo, CancellationToken cancellationToken, ITipografiaFinder? next = null)
        {
            if (tipo == 1)
            {
                return await ctx.Topografias.OfType<TopografiaPerna>()
                    .Include(t => t.Lateralidade)
                    .Include(t => t.Segmentacao)
                    .Include(t => t.RegiaoAnatomica)
                    .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            }
            return next != null ? await next.FindAsync(ctx, id, tipo, cancellationToken) : null;
        }
    }
}
