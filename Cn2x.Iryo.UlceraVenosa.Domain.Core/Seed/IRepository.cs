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
              
    }
}
