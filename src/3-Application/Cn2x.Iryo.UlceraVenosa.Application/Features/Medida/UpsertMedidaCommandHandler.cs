using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medidas;

public class UpsertMedidaCommandHandler : IRequestHandler<UpsertMedidaCommand, Guid>
{
    private readonly IRepository<Medida> _medidaRepository;

    public UpsertMedidaCommandHandler(IRepository<Medida> medidaRepository)
    {
        _medidaRepository = medidaRepository;
    }

    public async Task<Guid> Handle(UpsertMedidaCommand request, CancellationToken cancellationToken)
    {
        var existing = await _medidaRepository.GetByIdAsync(request.AvaliacaoUlceraId);
        if (existing != null)
        {
            existing.Comprimento = request.Comprimento;
            existing.Largura = request.Largura;
            existing.Profundidade = request.Profundidade;
            await _medidaRepository.UpdateAsync(existing);
            return existing.Id;
        }
        var medida = new Medida
        {
            Id = request.AvaliacaoUlceraId, // PK = AvaliacaoUlceraId
            AvaliacaoUlceraId = request.AvaliacaoUlceraId,
            Comprimento = request.Comprimento,
            Largura = request.Largura,
            Profundidade = request.Profundidade
        };
        await _medidaRepository.AddAsync(medida);
        return medida.Id;
    }
}