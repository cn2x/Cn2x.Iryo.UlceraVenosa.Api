using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IUlceraRepository : IBaseRepository<Ulcera>
{
    Task<IEnumerable<Ulcera>> GetByAvaliacaoIdAsync(Guid avaliacaoId);
    Task<IEnumerable<Ulcera>> GetWithDetailsAsync();
    Task<Ulcera?> GetWithDetailsByIdAsync(Guid id);
} 