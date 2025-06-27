namespace Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

/// <summary>
/// Interface base para repositórios
/// </summary>
/// <typeparam name="T">Tipo da entidade</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Obtém todas as entidades
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Obtém uma entidade por ID
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Adiciona uma nova entidade
    /// </summary>
    Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Atualiza uma entidade existente
    /// </summary>
    Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Remove uma entidade
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica se uma entidade existe
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
} 