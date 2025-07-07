using MediatR;
using System;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.ExsudatosDaUlcera;

public class UpsertExsudatoDaUlceraCommand : IRequest<bool>
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
} 