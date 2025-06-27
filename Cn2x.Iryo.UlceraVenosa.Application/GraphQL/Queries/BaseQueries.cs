using HotChocolate;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

/// <summary>
/// Classe base para queries GraphQL
/// </summary>
public abstract class BaseQueries
{
    /// <summary>
    /// Obtém todas as entidades paginadas
    /// </summary>
    protected async Task<PagedResult<T>> GetPagedAsync<T>(
        IBaseRepository<T> repository,
        int page = 1,
        int pageSize = 10) where T : class
    {
        // Implementação será adicionada quando os repositórios específicos forem criados
        throw new NotImplementedException("Método será implementado nos repositórios específicos");
    }

    /// <summary>
    /// Obtém todas as entidades
    /// </summary>
    protected async Task<IEnumerable<T>> GetAllAsync<T>(
        IBaseRepository<T> repository) where T : class
    {
        return await repository.GetAllAsync();
    }

    /// <summary>
    /// Obtém uma entidade por ID
    /// </summary>
    protected async Task<T?> GetByIdAsync<T>(
        Guid id,
        IBaseRepository<T> repository) where T : class
    {
        return await repository.GetByIdAsync(id);
    }
} 