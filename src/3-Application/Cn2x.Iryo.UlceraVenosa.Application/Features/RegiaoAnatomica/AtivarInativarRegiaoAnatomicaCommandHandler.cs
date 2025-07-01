using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class AtivarInativarRegiaoAnatomicaCommandHandler : IRequestHandler<AtivarInativarRegiaoAnatomicaCommand, bool>
{
    private readonly IRepository<Domain.Entities.RegiaoAnatomica> _repository;
    public AtivarInativarRegiaoAnatomicaCommandHandler(IRepository<Domain.Entities.RegiaoAnatomica> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarRegiaoAnatomicaCommand request, CancellationToken cancellationToken)
    {
        var regiao = await _repository.GetByIdAsync(request.Id);
        if (regiao == null) return false;
        regiao.Desativada = request.Desativar;
        await _repository.UpdateAsync(regiao);
        return true;
    }
} 