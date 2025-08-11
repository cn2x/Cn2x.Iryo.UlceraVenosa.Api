using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Commands;

public class UpsertProfissionalCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

