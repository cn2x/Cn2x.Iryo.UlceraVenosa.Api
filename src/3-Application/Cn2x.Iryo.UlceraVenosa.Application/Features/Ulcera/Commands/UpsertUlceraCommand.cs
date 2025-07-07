using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraCommand : IRequest<Guid>
{
    public Guid? Id { get; set; } // Se null ou Guid.Empty, cria; senão, atualiza
    public Guid PacienteId { get; set; }
    public List<Guid> Topografias { get; set; } = new();
} 