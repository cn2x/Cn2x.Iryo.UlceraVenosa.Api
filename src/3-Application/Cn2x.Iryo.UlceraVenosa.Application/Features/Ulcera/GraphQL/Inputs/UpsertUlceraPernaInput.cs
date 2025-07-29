using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

public class UpsertUlceraPernaInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public TopografiaPernaInput Topografia { get; set; } = null!;
    public CeapInput? ClassificacaoCeap { get; set; }
}
