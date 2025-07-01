using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class SearchUlceraByNomeQuery : IRequest<IEnumerable<Domain.Entities.Ulcera>>
{
    public string Nome { get; set; } = string.Empty;
} 