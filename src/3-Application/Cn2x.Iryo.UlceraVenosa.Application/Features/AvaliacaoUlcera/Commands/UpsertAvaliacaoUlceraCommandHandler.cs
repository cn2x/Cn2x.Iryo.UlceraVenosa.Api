using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;

public class UpsertAvaliacaoUlceraCommandHandler : IRequestHandler<UpsertAvaliacaoUlceraCommand, Guid>
{
    private readonly IAvaliacaoUlceraRepository _avaliacaoUlceraRepository;

    public UpsertAvaliacaoUlceraCommandHandler(IAvaliacaoUlceraRepository avaliacaoUlceraRepository)
    {
        _avaliacaoUlceraRepository = avaliacaoUlceraRepository;
    }

    public async Task<Guid> Handle(UpsertAvaliacaoUlceraCommand request, CancellationToken cancellationToken)
    {
        Cn2x.Iryo.UlceraVenosa.Domain.Entities.AvaliacaoUlcera? avaliacao = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            avaliacao = await _avaliacaoUlceraRepository.GetByIdAsync(request.Id.Value);
        }

        if (avaliacao is null)
        {
            // Criação
            var novaAvaliacao = new Cn2x.Iryo.UlceraVenosa.Domain.Entities.AvaliacaoUlcera
            {
                UlceraId = request.UlceraId,
                DataAvaliacao = request.DataAvaliacao,
                Duracao = request.Duracao,
                Caracteristicas = request.Caracteristicas,
                SinaisInflamatorios = request.SinaisInflamatorios,
                Medida = request.Medida,
                ClassificacaoCeap = request.ClassificacaoCeap
            };
            await _avaliacaoUlceraRepository.AddAsync(novaAvaliacao);
            await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return novaAvaliacao.Id;
        }
        else
        {
            // Atualização
            avaliacao.UlceraId = request.UlceraId;
            avaliacao.DataAvaliacao = request.DataAvaliacao;
            avaliacao.Duracao = request.Duracao;
            avaliacao.Caracteristicas = request.Caracteristicas;
            avaliacao.SinaisInflamatorios = request.SinaisInflamatorios;
            avaliacao.Medida = request.Medida;
            avaliacao.ClassificacaoCeap = request.ClassificacaoCeap;
            await _avaliacaoUlceraRepository.UpdateAsync(avaliacao);
            await _avaliacaoUlceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return avaliacao.Id;
        }
    }
}
