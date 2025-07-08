using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Topografia;

public class CreateTopografiaCommand : IRequest<Guid>
{
    public Guid UlceraId { get; set; }
    public Guid RegiaoId { get; set; }
    public Lateralidade Lado { get; set; } = Lateralidade.Direito;
} 