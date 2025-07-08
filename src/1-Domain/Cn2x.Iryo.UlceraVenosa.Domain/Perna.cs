using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain {
    public class Perna : Membro {
        public virtual ICollection<Segmentacao> Segmentos { get; set; } = new List<Segmentacao>();
        public virtual ICollection<RegiaoAnatomica> Regioes { get; set; } = new List<RegiaoAnatomica>();
    }
}