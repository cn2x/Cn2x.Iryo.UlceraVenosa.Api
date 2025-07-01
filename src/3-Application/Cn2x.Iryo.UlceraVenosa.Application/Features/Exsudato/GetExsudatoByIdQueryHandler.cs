using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class GetExsudatoByIdQueryHandler : IRequestHandler<GetExsudatoByIdQuery, Domain.Entities.Exsudato?>
{
    private readonly IRepository<Domain.Entities.Exsudato> _repository;
    public GetExsudatoByIdQueryHandler(IRepository<Domain.Entities.Exsudato> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Exsudato?> Handle(GetExsudatoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
} 