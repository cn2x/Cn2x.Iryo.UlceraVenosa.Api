using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class UpdateRegiaoAnatomicaCommandHandler : IRequestHandler<UpdateRegiaoAnatomicaCommand, bool>
{
    private readonly IRepository<Domain.Entities.RegiaoAnatomica> _repository;
    public UpdateRegiaoAnatomicaCommandHandler(IRepository<Domain.Entities.RegiaoAnatomica> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateRegiaoAnatomicaCommand request, CancellationToken cancellationToken)
    {
        var regiao = await _repository.GetByIdAsync(request.Id);
        if (regiao == null) return false;
        regiao.SegmentoId = request.SegmentoId;
        regiao.Limites = request.Limites;
        await _repository.UpdateAsync(regiao);
        return true;
    }
} 