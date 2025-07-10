using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {
    public class TopografiaPerna : Topografia {
        public Guid SegmentacaoId { get; set; }
        public required Segmentacao Segmentacao { get; set; }
        public Guid RegiaoAnatomicaId { get; set; }
        public required RegiaoAnatomica RegiaoAnatomica { get; set; }
        public override TopografiaEnum Tipo => TopografiaEnum.Perna;
    }
}