using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class AtivarInativarTopografiaCommandHandler : IRequestHandler<AtivarInativarTopografiaCommand, bool>
{
    private readonly IRepository<Domain.Entities.Topografia> _repository;
    public AtivarInativarTopografiaCommandHandler(IRepository<Domain.Entities.Topografia> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarTopografiaCommand request, CancellationToken cancellationToken)
    {
        var topografia = await _repository.GetByIdAsync(request.Id);
        if (topografia == null) return false;
        topografia.Desativada = request.Desativar;
        await _repository.UpdateAsync(topografia);
        return true;
    }
} 