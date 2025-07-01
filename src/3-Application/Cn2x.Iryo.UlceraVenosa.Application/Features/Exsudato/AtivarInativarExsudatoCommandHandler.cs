using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class AtivarInativarExsudatoCommandHandler : IRequestHandler<AtivarInativarExsudatoCommand, bool>
{
    private readonly IRepository<Domain.Entities.Exsudato> _repository;
    public AtivarInativarExsudatoCommandHandler(IRepository<Domain.Entities.Exsudato> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarExsudatoCommand request, CancellationToken cancellationToken)
    {
        var exsudato = await _repository.GetByIdAsync(request.Id);
        if (exsudato == null) return false;
        exsudato.Desativada = request.Desativar;
        await _repository.UpdateAsync(exsudato);
        return true;
    }
} 