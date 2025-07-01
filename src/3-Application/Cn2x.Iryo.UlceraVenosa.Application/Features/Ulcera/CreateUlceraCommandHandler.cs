using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class CreateUlceraCommandHandler : IRequestHandler<CreateUlceraCommand, Guid>
{
    private readonly IUlceraRepository _ulceraRepository;

    public CreateUlceraCommandHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<Guid> Handle(CreateUlceraCommand request, CancellationToken cancellationToken)
    {
        // Usar a factory para criar a úlcera com todas as validações
        var ulcera = UlceraFactory.Create(
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

        await _ulceraRepository.AddAsync(ulcera);
        await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return ulcera.Id;
    }
} 