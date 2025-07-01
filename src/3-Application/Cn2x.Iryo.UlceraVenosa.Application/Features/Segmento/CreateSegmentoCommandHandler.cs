using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Segmento;

public class CreateSegmentoCommandHandler : IRequestHandler<CreateSegmentoCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Segmento> _repository;
    public CreateSegmentoCommandHandler(IRepository<Domain.Entities.Segmento> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateSegmentoCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Segmento
        {
            Descricao = request.Descricao,
            Nome = request.Nome,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 