using HotChocolate;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;

/// <summary>
/// Classe base para mutations GraphQL
/// </summary>
public abstract class BaseMutations
{
    /// <summary>
    /// Cria uma nova entidade
    /// </summary>
    protected async Task<T> CreateAsync<T>(
        T entity,
        IBaseRepository<T> repository) where T : class
    {
        return await repository.AddAsync(entity);
    }

    /// <summary>
    /// Atualiza uma entidade existente
    /// </summary>
    protected async Task<T> UpdateAsync<T>(
        T entity,
        IBaseRepository<T> repository) where T : class
    {
        return await repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Remove uma entidade
    /// </summary>
    protected async Task<bool> DeleteAsync<T>(
        Guid id,
        IBaseRepository<T> repository) where T : class
    {
        return await repository.DeleteAsync(id);
    }
} 