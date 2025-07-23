using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

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

    public async Task<IEnumerable<Ulcera>> GetWithDetailsAsync()
    {
        return await _context.Ulceras
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
            .ToListAsync();
    }

    public async Task<Ulcera?> GetWithDetailsByIdAsync(Guid id)
    {
        return await _context.Ulceras
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public override async Task<Ulcera?> GetByIdAsync(Guid id)
    {
        return await _context.Ulceras
            .Include(u => u.Topografia)
            .ThenInclude(t => (t as TopografiaPerna)!.Lateralidade)
            .Include(u => u.Topografia)
            .ThenInclude(t => (t as TopografiaPerna)!.Segmentacao)
            .Include(u => u.Topografia)
            .ThenInclude(t => (t as TopografiaPerna)!.RegiaoAnatomica)
            .Include(u => u.Topografia)
            .ThenInclude(t => (t as TopografiaPe)!.Lateralidade)
            .Include(u => u.Topografia)
            .ThenInclude(t => (t as TopografiaPe)!.RegiaoTopograficaPe)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<PagedResult<Ulcera>> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
    {
        var query = _context.Ulceras
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
            .Where(u => !u.Desativada);

        // Aplicar filtro de busca se fornecido
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => 
                u.Paciente.Nome.Contains(searchTerm) ||
                u.Paciente.Cpf.Contains(searchTerm)
            );
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Ulcera>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<Ulcera>> SearchByPacienteNomeAsync(string nome)
    {
        return await _context.Ulceras
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
            .Where(u => !u.Desativada && u.Paciente.Nome.Contains(nome))
            .OrderByDescending(u => u.Id)
            .ToListAsync();
    }
}