using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {

    public enum TopografiaEnum : int { 
        Perna = 1, 
        Pes = 2
    }

    public abstract class Topografia : Entity<int> {
        public abstract TopografiaEnum Tipo { get;  }
        public int LateralidadeId { get; set; }
        public required Lateralidade Lateralidade { get; set; }
    }
}