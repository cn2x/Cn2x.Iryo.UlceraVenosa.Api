using System;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities
{
    public class UlceraSegmentacao
    {
        public Guid UlceraId { get; set; }
        public Ulcera Ulcera { get; set; }

        public int SegmentacaoId { get; set; }
        public Segmentacao Segmentacao { get; set; }
    }
}
