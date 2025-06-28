using System.Linq.Expressions;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core{
    public interface IRepositoryBase {
        Task<IQueryable<T>> BuscaPelaExpressao<T>(Expression<Func<T, bool>> expression, 
            params Expression<Func<T, object>>[] includes) where T : class;
        Task<IPageble<T>> BuscaPaginadaPelaExpressao<T>
            (Expression<Func<T, bool>> expression, int start = 1, int limit = 1, 
            params Expression<Func<T, object>>[] includes) where T : class;
    }
}