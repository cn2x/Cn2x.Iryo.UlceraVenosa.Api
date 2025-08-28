using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public async Task<Domain.Entities.Ulcera?> GetUlceraAsync(Guid id)
    {
        return await Ulceras
            .AsNoTracking()
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

    public async Task<PagedResult<Domain.Entities.Ulcera>> GetUlcerasAsync(int page = 1, int pageSize = 10, string? searchTerm = null)
    {
        var query = Ulceras
            .AsNoTracking()
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
            .Where(u => !u.Desativada);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => 
                u.Paciente.Nome.Contains(searchTerm) ||
                u.Paciente.Cpf.Contains(searchTerm) ||
                u.Paciente.Id.Equals(searchTerm)
            );
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Domain.Entities.Ulcera>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<UlceraWithTotalAvaliacoes>> GetUlcerasByPacienteAsync(Guid pacienteId)
    {
        return await Ulceras
            .AsNoTracking()
            .Include(u => u.Paciente)
            .Include(u => u.Avaliacoes)
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
            .Where(u => u.PacienteId == pacienteId && !u.Desativada)
            .Select(u => new UlceraWithTotalAvaliacoes
            {
                Ulcera = u,
                TotalAvaliacoes = u.Avaliacoes.Count
            })
            .ToListAsync();
    }
}

public class UlceraWithTotalAvaliacoes
{
    public Domain.Entities.Ulcera Ulcera { get; set; } = null!;
    public int TotalAvaliacoes { get; set; }
}
