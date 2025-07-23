using MediatR;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Pe;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraPeCommandHandler : IRequestHandler<UpsertUlceraPeCommand, Guid>
{
    private readonly IUlceraRepository _ulceraRepository;
    private readonly ApplicationDbContext _context;

    public UpsertUlceraPeCommandHandler(IUlceraRepository ulceraRepository, ApplicationDbContext context)
    {
        _ulceraRepository = ulceraRepository;
        _context = context;
    }

    public async Task<Guid> Handle(UpsertUlceraPeCommand request, CancellationToken cancellationToken)
    {
        var lateralidade = await _context.Lateralidades.FindAsync(new object[] { request.LateralidadeId }, cancellationToken);
        var regiao = await _context.RegioesAnatomicas.FindAsync(new object[] { request.RegiaoAnatomicaId }, cancellationToken);
        if (lateralidade == null || regiao == null)
            throw new Exception("Dados de topografia inválidos para pé");

        var regiaoTopograficaPe = await _context.RegioesTopograficasPe.FindAsync(new object[] { request.RegiaoTopograficaPeId }, cancellationToken);
        if (regiaoTopograficaPe == null)
            throw new Exception("Região topográfica do pé não encontrada");

        var topografia = new TopografiaPe
        {
            LateralidadeId = request.LateralidadeId,
            Lateralidade = lateralidade,
            RegiaoTopograficaPeId = request.RegiaoTopograficaPeId,
            RegiaoTopograficaPe = regiaoTopograficaPe
        };

        Cn2x.Iryo.UlceraVenosa.Domain.Entities.Ulcera? ulcera = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            ulcera = await _ulceraRepository.GetByIdAsync(request.Id.Value);
        }

        Ceap? ceap = Ceap.FromInput(request.ClassificacaoCeap);

        if (ulcera == null)
        {
            ulcera = new Cn2x.Iryo.UlceraVenosa.Domain.Entities.Ulcera
            {
                PacienteId = request.PacienteId,
                Topografia = topografia,
                Ceap = ceap
            };
            await _ulceraRepository.AddAsync(ulcera);
        }
        else
        {
            ulcera.Topografia = topografia;
            ulcera.Ceap = ceap;
            await _ulceraRepository.UpdateAsync(ulcera);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return ulcera.Id;
    }
}
