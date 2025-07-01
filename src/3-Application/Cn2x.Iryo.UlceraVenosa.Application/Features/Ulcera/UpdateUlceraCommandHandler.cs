using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class UpdateUlceraCommandHandler : IRequestHandler<UpdateUlceraCommand, bool>
{
    private readonly IUlceraRepository _ulceraRepository;

    public UpdateUlceraCommandHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<bool> Handle(UpdateUlceraCommand request, CancellationToken cancellationToken)
    {
        var ulcera = await _ulceraRepository.GetByIdAsync(request.Id);
        if (ulcera == null)
            return false;

        // Usar a factory para criar uma nova versão da úlcera com todas as validações
        var ulceraAtualizada = UlceraFactory.CreateForUpdate(
            request.Id,
            request.PacienteId,
            request.AvaliacaoId,
            request.Duracao,
            request.DataExame,
            request.ComprimentoCm,
            request.Largura,
            request.Profundidade,
            request.ClasseClinica,
            request.Etiologia,
            request.Anatomia,
            request.Patofisiologia,
            request.Caracteristicas,
            request.SinaisInflamatorios
        );

        // Manter as coleções existentes
        ulceraAtualizada.Topografias = ulcera.Topografias;
        ulceraAtualizada.Exsudatos = ulcera.Exsudatos;
        ulceraAtualizada.Imagens = ulcera.Imagens;

        await _ulceraRepository.UpdateAsync(ulceraAtualizada);
        await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
} 