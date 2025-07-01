using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class AtivarInativarUlceraCommandHandler : IRequestHandler<AtivarInativarUlceraCommand, bool>
{
    private readonly IUlceraRepository _ulceraRepository;

    public AtivarInativarUlceraCommandHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;        
    }

    public async Task<bool> Handle(AtivarInativarUlceraCommand request, CancellationToken cancellationToken)
    {
        var ulcera = await _ulceraRepository.GetByIdAsync(request.Id);
        if (ulcera == null)
            return false;

        ulcera.Desativada = !ulcera.Desativada;
        await _ulceraRepository.UpdateAsync(ulcera);
        await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
} 