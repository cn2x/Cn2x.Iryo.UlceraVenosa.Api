using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data
{
    public partial class ApplicationDbContext
    {
        /// <summary>
        /// Busca todas as regiões anatômicas ativas ordenadas por sigla
        /// </summary>
        public async Task<List<RegiaoAnatomica>> GetRegioesAnatomicasAtivasAsync(CancellationToken cancellationToken = default)
        {
            return await RegioesAnatomicas
                .Where(r => !r.Desativada)
                .OrderBy(r => r.Sigla)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Busca uma região anatômica por ID
        /// </summary>
        public async Task<RegiaoAnatomica?> GetRegiaoAnatomicaByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await RegioesAnatomicas
                .FirstOrDefaultAsync(r => r.Id == id && !r.Desativada, cancellationToken);
        }

        /// <summary>
        /// Busca uma região anatômica por sigla
        /// </summary>
        public async Task<RegiaoAnatomica?> GetRegiaoAnatomicaBySiglaAsync(string sigla, CancellationToken cancellationToken = default)
        {
            return await RegioesAnatomicas
                .FirstOrDefaultAsync(r => r.Sigla == sigla && !r.Desativada, cancellationToken);
        }
    }
} 