using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class ProfissionalRepository : BaseRepository<Profissional>, IProfissionalRepository
{
    public ProfissionalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Profissional?> GetByIdAsync(Guid id)
    {
        return await _context.Profissionais
            .FirstOrDefaultAsync(p => p.Id == id && !p.Desativada);
    }

    public async Task<IEnumerable<Profissional>> GetAllAsync()
    {
        return await _context.Profissionais
            .Where(p => !p.Desativada)
            .ToListAsync();
    }

    public async Task<Profissional> AddAsync(Profissional profissional)
    {
        await _context.Profissionais.AddAsync(profissional);
        await _context.SaveChangesAsync();
        return profissional;
    }

    public async Task<Profissional> UpdateAsync(Profissional profissional)
    {
        _context.Profissionais.Update(profissional);
        await _context.SaveChangesAsync();
        return profissional;
    }

    public async Task DeleteAsync(Guid id)
    {
        var profissional = await GetByIdAsync(id);
        if (profissional != null)
        {
            profissional.Desativada = true;
            await _context.SaveChangesAsync();
        }
    }
}
