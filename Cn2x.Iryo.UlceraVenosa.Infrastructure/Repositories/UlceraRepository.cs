using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class UlceraRepository : BaseRepository<Ulcera>, IUlceraRepository
{
    public UlceraRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ulcera>> GetByPacienteIdAsync(Guid pacienteId)
    {
        return await _context.Ulceras
            .Where(u => u.PacienteId == pacienteId)
            .ToListAsync();
    }

    public async Task<Ulcera?> GetWithClassificacaoAsync(Guid id)
    {
        return await _context.Ulceras
            .Include(u => u.ClassificacaoCeap)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Ulcera>> GetByAvaliacaoIdAsync(Guid avaliacaoId)
    {
        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.TopografiasNavigation)
            .Include(u => u.ExsudatosNavigation)
            .Where(u => u.AvaliacaoId == avaliacaoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ulcera>> GetWithDetailsAsync()
    {
        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.TopografiasNavigation)
            .Include(u => u.ExsudatosNavigation)
            .Include(u => u.Avaliacao)
            .ToListAsync();
    }

    public async Task<Ulcera?> GetWithDetailsByIdAsync(Guid id)
    {
        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.TopografiasNavigation)
            .Include(u => u.ExsudatosNavigation)
            .Include(u => u.Avaliacao)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
} 