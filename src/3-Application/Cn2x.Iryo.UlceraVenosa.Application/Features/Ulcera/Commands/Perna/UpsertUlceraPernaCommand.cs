using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Perna;

public class UpsertUlceraPernaCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid LateralidadeId { get; set; }
    public Guid SegmentacaoId { get; set; }
    public Guid RegiaoAnatomicaId { get; set; }
    public CeapInput? ClassificacaoCeap { get; set; }
}
