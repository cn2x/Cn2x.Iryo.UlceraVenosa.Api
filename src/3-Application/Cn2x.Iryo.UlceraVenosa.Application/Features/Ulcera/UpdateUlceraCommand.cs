using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera;

public class UpdateUlceraCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid AvaliacaoId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public decimal ComprimentoCm { get; set; }
    public decimal Largura { get; set; }
    public decimal Profundidade { get; set; }
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    
    // CEAP - todos obrigatórios
    public Clinica ClasseClinica { get; set; }
    public Etiologica Etiologia { get; set; }
    public Anatomica Anatomia { get; set; }
    public Patofisiologica Patofisiologia { get; set; }
} 