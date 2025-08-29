using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public async Task<IEnumerable<AvaliacaoUlcera>> GetAvaliacoesByPacienteAsync(Guid pacienteId)
    {
        return await AvaliacoesUlcera
            .AsNoTracking()
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Paciente)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Topografia)
                .ThenInclude(t => (t as TopografiaPerna)!.Lateralidade)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Topografia)
                .ThenInclude(t => (t as TopografiaPerna)!.Segmentacao)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Topografia)
                .ThenInclude(t => (t as TopografiaPerna)!.RegiaoAnatomica)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Topografia)
                .ThenInclude(t => (t as TopografiaPe)!.Lateralidade)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Topografia)
                .ThenInclude(t => (t as TopografiaPe)!.RegiaoTopograficaPe)
            .Include(a => a.Ulcera)
                .ThenInclude(u => u.Ceap)
            .Include(a => a.Profissional)
            .Include(a => a.Caracteristicas)
            .Include(a => a.SinaisInflamatorios)
            .Include(a => a.Medida)
            .Include(a => a.Imagens)
                .ThenInclude(i => i.Imagem)
            .Include(a => a.Exsudatos)
                .ThenInclude(e => e.Exsudato)
            .Where(a => a.Ulcera.PacienteId == pacienteId && !a.Ulcera.Desativada)
            .OrderByDescending(a => a.DataAvaliacao)
            .ToListAsync();
    }
}
