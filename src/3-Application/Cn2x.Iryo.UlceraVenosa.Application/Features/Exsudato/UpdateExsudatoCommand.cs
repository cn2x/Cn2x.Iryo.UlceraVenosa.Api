using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class UpdateExsudatoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UlceraId { get; set; }
    public Guid ExsudatoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
} 