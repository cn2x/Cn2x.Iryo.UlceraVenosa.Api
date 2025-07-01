using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class CreateRegiaoAnatomicaCommandHandler : IRequestHandler<CreateRegiaoAnatomicaCommand, Guid>
{
    private readonly IRepository<Domain.Entities.RegiaoAnatomica> _repository;
    public CreateRegiaoAnatomicaCommandHandler(IRepository<Domain.Entities.RegiaoAnatomica> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateRegiaoAnatomicaCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.RegiaoAnatomica
        {
            SegmentoId = request.SegmentoId,
            Limites = request.Limites,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 