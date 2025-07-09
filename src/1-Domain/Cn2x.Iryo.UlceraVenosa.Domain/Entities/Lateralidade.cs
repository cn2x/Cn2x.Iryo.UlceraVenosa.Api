using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class Lateralidade : Entity<int>
{
    public string Nome { get; set; } = string.Empty;
}
