using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

public class UpsertUlceraPeInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public TopografiaPeInput Topografia { get; set; } = null!;
    public CeapInput? ClassificacaoCeap { get; set; }
}

public class TopografiaPeInput
{
    public Guid LateralidadeId { get; set; }
    public Guid RegiaoAnatomicaId { get; set; }
    public Guid RegiaoTopograficaPeId { get; set; }
}
