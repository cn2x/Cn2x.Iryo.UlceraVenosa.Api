using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

public class RegiaoAnatomica : Entity<int>
{
    public string Sigla { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}
