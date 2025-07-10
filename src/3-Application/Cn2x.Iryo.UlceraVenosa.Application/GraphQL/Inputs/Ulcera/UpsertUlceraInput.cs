using System;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Inputs.Ulcera;

public class UpsertUlceraInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public int TopografiaId { get; set; }
    public int TipoTopografia { get; set; }
    public CeapInput? ClassificacaoCeap { get; set; }
}
