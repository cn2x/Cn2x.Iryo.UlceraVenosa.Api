using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

public class UpsertUlceraPernaInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public TopografiaPernaInput Topografia { get; set; } = null!;
    public CeapInput? ClassificacaoCeap { get; set; }
}

public class TopografiaPernaInput
{
    public Guid LateralidadeId { get; set; }
    public Guid SegmentacaoId { get; set; }
    public Guid RegiaoAnatomicaId { get; set; }
}
