using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class UpdateAvaliacaoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public string Diagnostico { get; set; } = string.Empty;
    public string Conduta { get; set; } = string.Empty;
} 