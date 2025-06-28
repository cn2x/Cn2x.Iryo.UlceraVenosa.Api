using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IAvaliacaoRepository : IBaseRepository<Avaliacao>
{
    Task<IEnumerable<Avaliacao>> GetByPacienteIdAsync(Guid pacienteId);
    Task<Avaliacao?> GetWithUlcerasAsync(Guid id);
    Task<IEnumerable<Avaliacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim);
} 