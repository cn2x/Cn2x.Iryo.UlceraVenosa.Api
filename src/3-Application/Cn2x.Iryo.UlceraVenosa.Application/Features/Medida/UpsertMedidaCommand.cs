using MediatR;
using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medidas;

public class UpsertMedidaCommand : IRequest<Guid>
{
    public Guid UlceraId { get; set; }
    public decimal? Comprimento { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Profundidade { get; set; }
} 