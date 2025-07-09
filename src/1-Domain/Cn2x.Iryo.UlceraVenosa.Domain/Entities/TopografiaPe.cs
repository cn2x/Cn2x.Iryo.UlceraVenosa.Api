using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {
    public class TopografiaPe : Topografia {
        public int RegiaoTopograficaPeId { get; set; }
        public RegiaoTopograficaPe RegiaoTopograficaPe { get; set; }
    }
}