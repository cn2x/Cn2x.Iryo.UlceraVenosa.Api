using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

public interface IUlceraRepository : IRepository<Ulcera>
{
    Task<IEnumerable<Ulcera>> GetWithDetailsAsync();
    Task<Ulcera?> GetWithDetailsByIdAsync(Guid id);
    
    // Métodos de paginação e busca
    Task<PagedResult<Ulcera>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);
    Task<Ulcera?> GetByPacienteIdSingleAsync(Guid pacienteId);
    Task<IEnumerable<Ulcera>> SearchByPacienteNomeAsync(string nome);
    
    // Método para buscar todas as úlceras de um paciente
    Task<IEnumerable<Ulcera>> GetByPacienteIdAsync(Guid pacienteId);
} 