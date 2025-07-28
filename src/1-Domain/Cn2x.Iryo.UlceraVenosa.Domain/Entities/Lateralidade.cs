using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class Lateralidade : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; set; } = string.Empty;
}
