using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Avaliacao;

public class CreateAvaliacaoCommand : IRequest<Guid>
{
    public Guid PacienteId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public string Diagnostico { get; set; } = string.Empty;
    public string Conduta { get; set; } = string.Empty;
} 