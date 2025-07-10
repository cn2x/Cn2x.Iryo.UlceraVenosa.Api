using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities {

    public enum TopografiaEnum : int { 
        Perna = 1, 
        Pe = 2
    }

    public abstract class Topografia : Entity<Guid> {
        public abstract TopografiaEnum Tipo { get;  }
        public Guid LateralidadeId { get; set; }
        public required Lateralidade Lateralidade { get; set; }
    }
}