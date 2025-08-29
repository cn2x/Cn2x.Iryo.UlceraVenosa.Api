using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public async Task<List<Lateralidade>> GetLateralidadesAtivasAsync(CancellationToken cancellationToken = default)
    {
        return await Lateralidades
            .Where(l => !l.Desativada)
            .OrderBy(l => l.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<Lateralidade?> GetLateralidadeByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Lateralidades
            .FirstOrDefaultAsync(l => l.Id == id && !l.Desativada, cancellationToken);
    }

    public async Task<Lateralidade?> GetLateralidadeByNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        return await Lateralidades
            .FirstOrDefaultAsync(l => l.Nome.ToLower() == nome.ToLower() && !l.Desativada, cancellationToken);
    }
} 