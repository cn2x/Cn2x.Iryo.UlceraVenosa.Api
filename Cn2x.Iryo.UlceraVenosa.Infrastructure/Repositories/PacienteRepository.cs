using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
{
    public PacienteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Paciente?> GetByCpfAsync(string cpf)
    {
        return await _context.Pacientes
            .FirstOrDefaultAsync(p => p.Cpf == cpf);
    }

    public async Task<IEnumerable<Paciente>> GetAtivosAsync()
    {
        return await _context.Pacientes
            .Where(p => p.Ativo)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Paciente?> GetWithAvaliacoesAsync(Guid id)
    {
        return await _context.Pacientes
            .Include(p => p.Avaliacoes)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
} 