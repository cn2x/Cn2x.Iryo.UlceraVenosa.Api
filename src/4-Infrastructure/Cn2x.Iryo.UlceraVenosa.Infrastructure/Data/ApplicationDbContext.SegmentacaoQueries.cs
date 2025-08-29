using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data
{
    public partial class ApplicationDbContext
    {
        /// <summary>
        /// Busca todas as segmentações ativas ordenadas por sigla
        /// </summary>
        public async Task<List<Segmentacao>> GetSegmentacoesAtivasAsync(CancellationToken cancellationToken = default)
        {
            return await Segmentacoes
                .Where(s => !s.Desativada)
                .OrderBy(s => s.Sigla)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Busca uma segmentação por ID
        /// </summary>
        public async Task<Segmentacao?> GetSegmentacaoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Segmentacoes
                .FirstOrDefaultAsync(s => s.Id == id && !s.Desativada, cancellationToken);
        }

        /// <summary>
        /// Busca uma segmentação por sigla
        /// </summary>
        public async Task<Segmentacao?> GetSegmentacaoBySiglaAsync(string sigla, CancellationToken cancellationToken = default)
        {
            return await Segmentacoes
                .FirstOrDefaultAsync(s => s.Sigla == sigla && !s.Desativada, cancellationToken);
        }
    }
} 