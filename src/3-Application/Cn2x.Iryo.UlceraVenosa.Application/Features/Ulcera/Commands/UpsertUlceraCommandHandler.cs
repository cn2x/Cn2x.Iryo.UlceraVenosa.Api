using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraCommandHandler : IRequestHandler<UpsertUlceraCommand, Guid>
{
    private readonly IUlceraRepository _ulceraRepository;

    public UpsertUlceraCommandHandler(IUlceraRepository ulceraRepository)
    {
        _ulceraRepository = ulceraRepository;
    }

    public async Task<Guid> Handle(UpsertUlceraCommand request, CancellationToken cancellationToken)
    {
        Cn2x.Iryo.UlceraVenosa.Domain.Entities.Ulcera? ulcera = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            ulcera = await _ulceraRepository.GetByIdAsync(request.Id.Value);
        }

        if (ulcera == null)
        {
            // Criação
            var novaUlcera = UlceraFactory.Create(request.PacienteId);
            novaUlcera.Topografias = request.Topografias.Select(id => new Cn2x.Iryo.UlceraVenosa.Domain.Entities.Topografia { Id = id }).ToList();
            await _ulceraRepository.AddAsync(novaUlcera);
            await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return novaUlcera.Id;
        }
        else
        {
            // Atualização
            var ulceraAtualizada = UlceraFactory.CreateForUpdate(
                ulcera.Id,
                request.PacienteId
            );
            ulceraAtualizada.Topografias = request.Topografias.Select(id => new Cn2x.Iryo.UlceraVenosa.Domain.Entities.Topografia { Id = id }).ToList();
            await _ulceraRepository.UpdateAsync(ulceraAtualizada);
            await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return ulceraAtualizada.Id;
        }
    }
} 