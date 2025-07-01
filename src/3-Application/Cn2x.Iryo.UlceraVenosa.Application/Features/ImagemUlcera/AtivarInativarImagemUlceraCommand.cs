using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class AtivarInativarImagemUlceraCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Desativar { get; set; }
} 