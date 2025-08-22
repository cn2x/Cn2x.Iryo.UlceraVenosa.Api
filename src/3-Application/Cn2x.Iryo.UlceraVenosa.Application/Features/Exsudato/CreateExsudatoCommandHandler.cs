using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class CreateExsudatoCommandHandler : IRequestHandler<CreateExsudatoCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Exsudato> _repository;
    public CreateExsudatoCommandHandler(IRepository<Domain.Entities.Exsudato> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateExsudatoCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Exsudato
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Desativada = false
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
} 