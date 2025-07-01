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
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Where(u => u.AvaliacaoId == avaliacaoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ulcera>> GetWithDetailsAsync()
    {
        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Include(u => u.Avaliacao)
            .ToListAsync();
    }

    public async Task<Ulcera?> GetWithDetailsByIdAsync(Guid id)
    {
        return await _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Include(u => u.Avaliacao)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<PagedResult<Ulcera>> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
    {
        var query = _context.Ulceras
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Include(u => u.Avaliacao)
            .Include(u => u.Paciente)
            .Where(u => !u.Desativada);

        // Aplicar filtro de busca se fornecido
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => 
                u.Paciente.Nome.Contains(searchTerm) ||
                u.Paciente.Cpf.Contains(searchTerm) ||
                u.Duracao.Contains(searchTerm)
            );
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(u => u.DataExame)
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
            .Include(u => u.Caracteristicas)
            .Include(u => u.SinaisInflamatorios)
            .Include(u => u.ClassificacaoCeap)
            .Include(u => u.Topografias)
            .Include(u => u.Exsudatos)
            .Include(u => u.Imagens)
            .Include(u => u.Avaliacao)
            .Include(u => u.Paciente)
            .Where(u => !u.Desativada && u.Paciente.Nome.Contains(nome))
            .OrderByDescending(u => u.DataExame)
            .ToListAsync();
    }
} 