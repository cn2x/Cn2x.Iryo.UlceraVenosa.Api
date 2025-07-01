using Beyond.IPO.Domain.Core.Specification;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using System.Linq.Expressions;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Specifications {
    public class RegiaoAnatomicaBySegmentoSpecification : Specification<RegiaoAnatomica>
    {
        private readonly Guid _segmentoId;
        public RegiaoAnatomicaBySegmentoSpecification(Guid segmentoId)
        {
            _segmentoId = segmentoId;
        }
        public override Expression<Func<RegiaoAnatomica, bool>> SatisfiedBy()
            => r => r.SegmentoId == _segmentoId;
    }
}
