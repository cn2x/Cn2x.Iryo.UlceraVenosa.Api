using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Perna;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraPernaCommandHandler : IRequestHandler<UpsertUlceraPernaCommand, Guid>
{
    private readonly IUlceraRepository _ulceraRepository;
    private readonly ApplicationDbContext _context;

    public UpsertUlceraPernaCommandHandler(IUlceraRepository ulceraRepository, ApplicationDbContext context)
    {
        _ulceraRepository = ulceraRepository;
        _context = context;
    }

    public async Task<Guid> Handle(UpsertUlceraPernaCommand request, CancellationToken cancellationToken)
    {
        // Busca entidades relacionadas
        var lateralidade = await _context.Lateralidades.FindAsync(new object[] { request.LateralidadeId }, cancellationToken);
        var segmentacao = await _context.Segmentacoes.FindAsync(new object[] { request.SegmentacaoId }, cancellationToken);
        var regiao = await _context.RegioesAnatomicas.FindAsync(new object[] { request.RegiaoAnatomicaId }, cancellationToken);
        if (lateralidade == null || segmentacao == null || regiao == null)
            throw new Exception("Dados de topografia inválidos para perna");

        // Monta topografia
        var topografia = new TopografiaPerna
        {
            LateralidadeId = request.LateralidadeId,
            SegmentacaoId = request.SegmentacaoId,
            RegiaoAnatomicaId = request.RegiaoAnatomicaId,
            Lateralidade = lateralidade,
            Segmentacao = segmentacao,
            RegiaoAnatomica = regiao
        };

        Domain.Entities.Ulcera? ulcera = null;
        if (request.Id != null && request.Id != Guid.Empty)
        {
            ulcera = await _ulceraRepository.GetByIdAsync(request.Id.Value);
        }

        Ceap? ceap = Ceap.FromInput(request.ClassificacaoCeap);

        if (ulcera == null)
        {
            ulcera = new Cn2x.Iryo.UlceraVenosa.Domain.Entities.Ulcera {
                PacienteId = request.PacienteId,
                Topografia = topografia,
                Ceap = ceap,
                Id = request.Id??Guid.NewGuid()
            };
            await _ulceraRepository.AddAsync(ulcera);
        }
        else
        {
            ulcera.Topografia = topografia;
            ulcera.Ceap = ceap;
            await _ulceraRepository.UpdateAsync(ulcera);
        }
        var xReturn = await _ulceraRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return ulcera.Id;
    }
}
