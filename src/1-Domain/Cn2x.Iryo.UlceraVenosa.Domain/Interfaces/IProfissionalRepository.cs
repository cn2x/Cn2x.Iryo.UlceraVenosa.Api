using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IProfissionalRepository : IRepository<Profissional>
{
    Task<Profissional?> GetByIdAsync(Guid id);
    Task<IEnumerable<Profissional>> GetAllAsync();
    Task<Profissional> AddAsync(Profissional profissional);
    Task<Profissional> UpdateAsync(Profissional profissional);
    Task DeleteAsync(Guid id);
}
