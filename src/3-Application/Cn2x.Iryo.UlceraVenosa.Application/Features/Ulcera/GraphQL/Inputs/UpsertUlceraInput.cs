using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

public class UpsertUlceraInput
{
    public Guid? Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid TopografiaId { get; set; }
    public int TipoTopografia { get; set; }
    public CeapInput? ClassificacaoCeap { get; set; }
} 