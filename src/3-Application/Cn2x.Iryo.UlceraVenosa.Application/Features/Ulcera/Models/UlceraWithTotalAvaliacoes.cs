using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Models;

public class UlceraWithTotalAvaliacoes
{
    public Domain.Entities.Ulcera Ulcera { get; set; } = null!;
    public int TotalAvaliacoes { get; set; }
}
