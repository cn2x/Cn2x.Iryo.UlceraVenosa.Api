using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.RegiaoAnatomica;

public class UpdateRegiaoAnatomicaCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid SegmentoId { get; set; }
    public string Limites { get; set; } = string.Empty;
} 