using System;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities
{
    public class UlceraRegiaoTopograficaPe
    {
        public Guid UlceraId { get; set; }
        public Ulcera Ulcera { get; set; } = null!;

        public int RegiaoTopograficaPeId { get; set; }
        public RegiaoTopograficaPe RegiaoTopograficaPe { get; set; } = null!;
    }
}
