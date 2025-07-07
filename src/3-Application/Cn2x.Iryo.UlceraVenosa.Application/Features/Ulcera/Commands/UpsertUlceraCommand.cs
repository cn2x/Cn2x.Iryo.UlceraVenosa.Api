using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.Commands;

public class UpsertUlceraCommand : IRequest<Guid>
{
    public Guid? Id { get; set; } // Se null ou Guid.Empty, cria; senão, atualiza
    public Guid PacienteId { get; set; }
    public string Duracao { get; set; } = string.Empty;
    public DateTime DataExame { get; set; }
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    public Clinica ClasseClinica { get; set; }
    public Etiologica Etiologia { get; set; }
    public Anatomica Anatomia { get; set; }
    public Patofisiologica Patofisiologia { get; set; }
} 