using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IPacienteRepository : IRepository<Paciente>
{
    Task<IEnumerable<Paciente>> GetAtivosAsync();
    Task<Paciente?> GetWithAvaliacoesAsync(Guid id);
    
    Task<Paciente?> GetWithCpfAsync(string cpf);
    
    Task SaveAsync(Paciente paciente);
    
    Task Update(Paciente paciente);
} 