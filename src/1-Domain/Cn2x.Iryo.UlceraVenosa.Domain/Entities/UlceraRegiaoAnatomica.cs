using System;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities
{
    public class UlceraRegiaoAnatomica
    {
        public Guid UlceraId { get; set; }
        public Ulcera Ulcera { get; set; } = null!;

        public int RegiaoAnatomicaId { get; set; }
        public RegiaoAnatomica RegiaoAnatomica { get; set; } = null!;
    }
}
