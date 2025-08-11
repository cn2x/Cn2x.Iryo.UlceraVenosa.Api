using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Commands;

public class DeleteProfissionalCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

