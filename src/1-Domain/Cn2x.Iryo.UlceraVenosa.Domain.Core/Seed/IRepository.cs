using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Beyond.IPO.Domain.Core.Specification;
using System.Linq;
using System.Linq.Expressions;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core
{
    public interface IRepository {
        IUnitOfWork UnitOfWork { get; }

        Task<IQueryable<M?>>
            FindFilterByExpression<M>(Expression<Func<M, bool>> expression)
            where M : class, IEntity<Guid>;

        //Task<IQueryable<M?>> 
        //    FindFilterByExpression<M, TProperty>(Expression<Func<M, bool>> expression,
        //        params Expression<Func<M, TProperty>>[] includes);
    }

    public interface IRepository<T> : IRepository where T : IAggregateRoot
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
}
