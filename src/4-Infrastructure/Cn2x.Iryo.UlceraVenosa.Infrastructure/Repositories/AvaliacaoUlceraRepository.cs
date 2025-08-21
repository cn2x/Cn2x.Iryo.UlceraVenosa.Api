using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class AvaliacaoUlceraRepository : BaseRepository<AvaliacaoUlcera>, IAvaliacaoUlceraRepository
{
    public AvaliacaoUlceraRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<AvaliacaoUlcera?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(a => a.Exsudatos)
                .ThenInclude(e => e.Exsudato)
            .Include(a => a.Imagens)
                .ThenInclude(i => i.Imagem)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
