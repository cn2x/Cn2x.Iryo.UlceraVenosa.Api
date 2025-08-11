using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Queries;

public class GetProfissionalQuery : IRequest<Domain.Entities.Profissional?>
{
    public Guid Id { get; set; }
}

