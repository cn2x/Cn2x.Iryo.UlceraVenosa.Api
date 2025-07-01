using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class SearchUlceraByNomeQueryHandler : IRequestHandler<SearchUlceraByNomeQuery, IEnumerable<Domain.Entities.Ulcera>>
{
    private readonly IUlceraRepository _ulceraRepository;

    public SearchUlceraByNomeQueryHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Ulcera>> Handle(SearchUlceraByNomeQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return new List<Domain.Entities.Ulcera>();

        return await _ulceraRepository.SearchByPacienteNomeAsync(request.Nome);
    }
} 