using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato;

public class AtivarInativarExsudatoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Desativar { get; set; }
} 