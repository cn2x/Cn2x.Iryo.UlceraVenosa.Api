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

    public async Task<IEnumerable<Paciente>> GetAtivosAsync()
    {
        return await _context.Pacientes
            .Where(p => !p.Desativada)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Paciente?> GetWithAvaliacoesAsync(Guid id)
    {
        return await _context.Pacientes
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Paciente?> GetWithCpfAsync(string cpf)
    {
        return await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
    }

    public async Task SaveAsync(Paciente paciente)
    {
        await _context.Pacientes.AddAsync(paciente);
    }

    public Task Update(Paciente paciente)
    {
        _context.Pacientes.Update(paciente);
        return Task.CompletedTask;
    }
} 