using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class CreateExsudatoCommand : IRequest<Guid>
{
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
    public string Descricao { get; set; } = string.Empty;
} 