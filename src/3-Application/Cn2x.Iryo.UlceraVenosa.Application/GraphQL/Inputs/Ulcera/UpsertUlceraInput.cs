using System;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

public class UpsertUlceraInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public List<Guid> Topografias { get; set; } = new();
    public CeapInput? ClassificacaoCeap { get; set; }
}
