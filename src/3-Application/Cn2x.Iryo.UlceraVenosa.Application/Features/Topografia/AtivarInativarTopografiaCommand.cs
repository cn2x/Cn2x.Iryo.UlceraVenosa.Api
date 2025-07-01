using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class AtivarInativarTopografiaCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Desativar { get; set; }
} 