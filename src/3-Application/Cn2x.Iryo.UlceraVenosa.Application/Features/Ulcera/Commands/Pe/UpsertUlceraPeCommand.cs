using MediatR;
using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands.Pe;

public class UpsertUlceraPeCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid LateralidadeId { get; set; }
    public Guid RegiaoAnatomicaId { get; set; }
    public Guid RegiaoTopograficaPeId { get; set; }
    public Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs.CeapInput? ClassificacaoCeap { get; set; }
}
