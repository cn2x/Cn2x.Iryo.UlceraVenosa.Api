using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {
    public class TopografiaPerna : Topografia {
        public required Guid SegmentacaoId { get; set; }
        public  Segmentacao Segmentacao { get; set; }
        public required Guid RegiaoAnatomicaId { get; set; }
        public  RegiaoAnatomica RegiaoAnatomica { get; set; }
        public override TopografiaEnum Tipo => TopografiaEnum.Perna;
    }
}