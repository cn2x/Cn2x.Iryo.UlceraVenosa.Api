using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medida;

public class UpsertMedidaCommandHandler : IRequestHandler<UpsertMedidaCommand, Guid>
{
    private readonly IAvaliacaoUlceraRepository _avaliacaoUlceraRepository;

    public UpsertMedidaCommandHandler(IAvaliacaoUlceraRepository avaliacaoUlceraRepository)
    {
        _avaliacaoUlceraRepository = avaliacaoUlceraRepository;
    }

    public async Task<Guid> Handle(UpsertMedidaCommand request, CancellationToken cancellationToken)
    {
        var avaliacao = await _avaliacaoUlceraRepository.GetByIdAsync(request.AvaliacaoUlceraId);
        if (avaliacao == null)
            throw new Exception($"Avaliação de úlcera {request.AvaliacaoUlceraId} não encontrada");

        // Sempre cria ou atualiza a Medida embutida
        avaliacao.Medida = new Domain.ValueObjects.Medida
        {
            Comprimento = request.Comprimento,
            Largura = request.Largura,
            Profundidade = request.Profundidade
        };
        await _avaliacaoUlceraRepository.UpdateAsync(avaliacao);
        await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return avaliacao.Id;
    }
}