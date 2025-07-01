using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class AtivarInativarAvaliacaoCommandHandler : IRequestHandler<AtivarInativarAvaliacaoCommand, bool>
{
    private readonly IRepository<Domain.Entities.Avaliacao> _repository;
    public AtivarInativarAvaliacaoCommandHandler(IRepository<Domain.Entities.Avaliacao> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtivarInativarAvaliacaoCommand request, CancellationToken cancellationToken)
    {
        var avaliacao = await _repository.GetByIdAsync(request.Id);
        if (avaliacao == null) return false;
        avaliacao.Desativada = request.Desativar;
        await _repository.UpdateAsync(avaliacao);
        return true;
    }
} 