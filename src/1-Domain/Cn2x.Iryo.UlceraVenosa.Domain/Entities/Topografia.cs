using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {
    public abstract class Topografia : Entity<int> {
        public int LateralidadeId { get; set; }
        public required Lateralidade Lateralidade { get; set; }
    }
}