using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class UpdateExsudatoCommandHandler : IRequestHandler<UpdateExsudatoCommand, bool>
{
    private readonly IRepository<Domain.Entities.Exsudato> _repository;
    public UpdateExsudatoCommandHandler(IRepository<Domain.Entities.Exsudato> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateExsudatoCommand request, CancellationToken cancellationToken)
    {
        var exsudato = await _repository.GetByIdAsync(request.Id);
        if (exsudato == null) return false;
        exsudato.Descricao = request.Descricao;
        await _repository.UpdateAsync(exsudato);
        return true;
    }
} 