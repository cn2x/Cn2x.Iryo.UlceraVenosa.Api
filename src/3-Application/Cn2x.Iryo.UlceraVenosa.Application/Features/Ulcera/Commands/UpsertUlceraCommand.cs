using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;



public class UpsertUlceraCommand : IRequest<Guid>
{
    public Guid? Id { get; set; } // Se null ou Guid.Empty, cria; senão, atualiza
    public Guid PacienteId { get; set; }
    public TopografiaEnum TipoTopografia { get; set; }
    public Guid TopografiaId { get; set; }
    public Ceap? ClassificacaoCeap { get; set; }
}