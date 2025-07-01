using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class AtivarInativarAvaliacaoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Desativar { get; set; }
} 