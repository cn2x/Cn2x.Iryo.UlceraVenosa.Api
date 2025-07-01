using HotChocolate;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

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
        IRepository<T> repository) where T : class, IAggregateRoot
    {
        return await repository.AddAsync(entity);
    }

    /// <summary>
    /// Atualiza uma entidade existente
    /// </summary>
    protected async Task<T> UpdateAsync<T>(
        T entity,
        IRepository<T> repository) where T : class, IAggregateRoot
    {
        return await repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Remove uma entidade
    /// </summary>
    protected async Task<bool> DeleteAsync<T>(
        Guid id,
        IRepository<T> repository) where T : class, IAggregateRoot
    {
        return await repository.DeleteAsync(id);
    }
} 