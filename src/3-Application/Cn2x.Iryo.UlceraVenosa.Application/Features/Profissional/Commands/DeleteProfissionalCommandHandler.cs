using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Profissional.Commands;

public class DeleteProfissionalCommandHandler : IRequestHandler<DeleteProfissionalCommand, bool>
{
    private readonly IProfissionalRepository _profissionalRepository;

    public DeleteProfissionalCommandHandler(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task<bool> Handle(DeleteProfissionalCommand request, CancellationToken cancellationToken)
    {
        var profissional = await _profissionalRepository.GetByIdAsync(request.Id);
        
        if (profissional == null)
            return false;

        await _profissionalRepository.DeleteAsync(request.Id);
        await _profissionalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}

