using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core{

    [ExcludeFromCodeCoverage]
    public class Pageble<TEntity> : IPageble<TEntity>
    {        
        private readonly List<TEntity> _lista;
        public Pageble()
        {
            _lista = new List<TEntity>();
        }
        public Pageble(IQueryable<TEntity> source, int start, int limit) : this()
        {
            int total = source.Count();
            Total = total;
            this.PageNumber = total / limit;

            if (total % limit > 0)
                PageNumber++;

            start -= 1;
            this.Limit = limit;
            this.Start = start;
            _lista.AddRange(source.Skip(start * limit).Take(limit).ToList());
        }

        public Pageble(IList<TEntity> source, int start, int limit)
        {
            Total = source.Count();
            PageNumber = Total / limit;

            if (Total % limit > 0)
                PageNumber++;

            this.Limit = limit;
            this.Start = start;
            _lista.AddRange(source.Skip(start * limit).Take(limit).ToList());
        }

        public Pageble(IEnumerable<TEntity> source, int start, int limit, int total)
        {
            Total = total;
            PageNumber = Total / limit;

            if (Total % limit > 0)
                PageNumber++;

            this.Limit = limit;
            this.Start = start;
            _lista.AddRange(source);
        }

        public int Start           { get; private set; }
        public int Limit           { get; private set; }
        public int PageNumber   { get; private set; }
        public int Total            { get; private set; }

        public bool Preview
        {
            get { return (Start > 0); }
        }

        public bool Next
        {
            get { return (Start + 1 < PageNumber); }
        }

        public IEnumerator<TEntity> GetEnumerator() {
            return _lista.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _lista.GetEnumerator(); 
        }
    }
}
