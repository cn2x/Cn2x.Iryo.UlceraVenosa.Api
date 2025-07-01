using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ImagemUlcera;

public class AtivarInativarImagemUlceraCommandHandler : IRequestHandler<AtivarInativarImagemUlceraCommand, bool>
{
    private readonly IRepository<Domain.Entities.ImagemUlcera> _repository;
    public AtivarInativarImagemUlceraCommandHandler(IRepository<Domain.Entities.ImagemUlcera> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarImagemUlceraCommand request, CancellationToken cancellationToken)
    {
        var imagem = await _repository.GetByIdAsync(request.Id);
        if (imagem == null) return false;
        imagem.Desativada = request.Desativar;
        await _repository.UpdateAsync(imagem);
        return true;
    }
} 