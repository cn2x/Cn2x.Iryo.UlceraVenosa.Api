using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class CreateTopografiaCommandHandler : IRequestHandler<CreateTopografiaCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Topografia> _repository;
    public CreateTopografiaCommandHandler(IRepository<Domain.Entities.Topografia> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTopografiaCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Topografia
        {
            UlceraId = request.UlceraId,
            RegiaoId = request.RegiaoId,
            Lado = request.Lado,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 