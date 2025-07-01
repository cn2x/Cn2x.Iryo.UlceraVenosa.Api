using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class CreateRegiaoAnatomicaCommand : IRequest<Guid>
{
    public Guid SegmentoId { get; set; }
    public string Limites { get; set; } = string.Empty;
} 