using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class AtivarInativarUlceraCommand : IRequest<bool>
{
    public Guid Id { get; set; }
} 