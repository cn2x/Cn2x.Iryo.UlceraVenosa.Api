using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using System;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data {
    public interface ITipografiaFinder
    {
        Task<Topografia?> FindAsync(ApplicationDbContext ctx, Guid id, int tipo, CancellationToken cancellationToken, ITipografiaFinder? next = null);
    }
}
