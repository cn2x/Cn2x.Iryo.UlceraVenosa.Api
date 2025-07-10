using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data {
    public interface ITipografiaFinder
    {
        Task<Topografia?> FindAsync(ApplicationDbContext ctx, int id, int tipo, CancellationToken cancellationToken, ITipografiaFinder? next = null);
    }
}
