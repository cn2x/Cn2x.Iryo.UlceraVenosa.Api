namespace Cn2x.Iryo.UlceraVenosa.Domain.Core {
    public interface IPageble<TEntity> : IEnumerable<TEntity> 
    {
        int Start { get; }
        int Limit { get; }        
        int PageNumber { get; }
        int Total { get; }
        bool Preview { get; }
        bool Next { get; }
    }   
}
