using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class SearchUlceraByCpfQuery : IRequest<IEnumerable<Domain.Entities.Ulcera>>
{
    public string Cpf { get; set; } = string.Empty;
} 