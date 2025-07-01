using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Segmento;

public class CreateSegmentoCommand : IRequest<Guid>
{
    public string Descricao { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
} 