using MediatR;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraCommandHandler : IRequestHandler<UpsertUlceraCommand, Guid>
{
    private readonly IUlceraRepository _ulceraRepository;
    private readonly ApplicationDbContext _context;

    public UpsertUlceraCommandHandler(IUlceraRepository ulceraRepository, ApplicationDbContext context)
    {
        _ulceraRepository = ulceraRepository;
        _context = context;
    }

    public async Task<Guid> Handle(UpsertUlceraCommand request, CancellationToken cancellationToken)
    {
        var topografia = await _context
            .FindTipografiaFilterByTypeAsync(cancellationToken, request.TopografiaId, (int)request.TipoTopografia);
            
        if (topografia is null)
            throw new Exception("Topografia principal não encontrada");

        Cn2x.Iryo.UlceraVenosa.Domain.Entities.Ulcera? ulcera = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            ulcera = await _ulceraRepository.GetByIdAsync(request.Id.Value);
        }

        Ceap? ceap = request.ClassificacaoCeap;

        if (ulcera is null)
        {
            // Criação
            var novaUlcera = UlceraFactory.Create(request.PacienteId, topografia, ceap);
            await _ulceraRepository.AddAsync(novaUlcera);
            await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return novaUlcera.Id;
        }
        else
        {
            // Atualização
            var ulceraAtualizada = UlceraFactory.CreateForUpdate(
                ulcera.Id,
                request.PacienteId,
                topografia,
                ceap
            );
            await _ulceraRepository.UpdateAsync(ulceraAtualizada);
            await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return ulceraAtualizada.Id;
        }
    }
}