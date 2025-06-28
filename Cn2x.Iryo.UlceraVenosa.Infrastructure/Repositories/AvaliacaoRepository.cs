using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class AvaliacaoRepository : BaseRepository<Avaliacao>, IAvaliacaoRepository
{
    public AvaliacaoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Avaliacao>> GetByPacienteIdAsync(Guid pacienteId)
    {
        return await _context.Avaliacoes
            .Include(a => a.Ulceras)
            .Where(a => a.PacienteId == pacienteId)
            .OrderByDescending(a => a.DataAvaliacao)
            .ToListAsync();
    }

    public async Task<Avaliacao?> GetWithUlcerasAsync(Guid id)
    {
        return await _context.Avaliacoes
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.Caracteristicas)
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.SinaisInflamatorios)
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.ClassificacaoCeap)
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.Topografias)
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.Exsudatos)
            .Include(a => a.Ulceras)
                .ThenInclude(u => u.Imagens)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Avaliacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
    {
        return await _context.Avaliacoes
            .Include(a => a.Paciente)
            .Include(a => a.Ulceras)
            .Where(a => a.DataAvaliacao >= inicio && a.DataAvaliacao <= fim)
            .OrderByDescending(a => a.DataAvaliacao)
            .ToListAsync();
    }
} 