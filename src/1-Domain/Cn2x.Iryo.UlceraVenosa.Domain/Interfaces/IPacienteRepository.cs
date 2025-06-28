using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IPacienteRepository : IBaseRepository<Paciente>
{
    Task<Paciente?> GetByCpfAsync(string cpf);
    Task<IEnumerable<Paciente>> GetAtivosAsync();
    Task<Paciente?> GetWithAvaliacoesAsync(Guid id);
} 