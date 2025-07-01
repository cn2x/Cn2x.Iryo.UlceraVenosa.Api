using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class UpdateTopografiaCommandHandler : IRequestHandler<UpdateTopografiaCommand, bool>
{
    private readonly IRepository<Domain.Entities.Topografia> _repository;
    public UpdateTopografiaCommandHandler(IRepository<Domain.Entities.Topografia> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTopografiaCommand request, CancellationToken cancellationToken)
    {
        var topografia = await _repository.GetByIdAsync(request.Id);
        if (topografia == null) return false;
        topografia.UlceraId = request.UlceraId;
        topografia.RegiaoId = request.RegiaoId;
        topografia.Lado = request.Lado;
        await _repository.UpdateAsync(topografia);
        return true;
    }
} 