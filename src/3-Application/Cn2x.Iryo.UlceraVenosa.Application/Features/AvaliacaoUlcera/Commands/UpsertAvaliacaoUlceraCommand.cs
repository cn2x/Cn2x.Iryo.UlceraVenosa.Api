using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;

public class UpsertAvaliacaoUlceraCommand : IRequest<Guid>
{
    public Guid? Id { get; set; } // Se null ou Guid.Empty, cria; senão, atualiza
    public Guid UlceraId { get; set; }
    public Guid ProfissionalId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public int MesesDuracao { get; set; } // duração em meses desde o surgimento da úlcera
    public Caracteristicas Caracteristicas { get; set; } = new();
    public SinaisInflamatorios SinaisInflamatorios { get; set; } = new();
    public Domain.ValueObjects.Medida? Medida { get; set; }
    
    // Upload de arquivo como base64 (mais compatível)
    public string? Arquivo { get; set; }
    
    // Metadados da imagem
    public string? DescricaoImagem { get; set; }
    public DateTime? DataCapturaImagem { get; set; }
    
    public List<Guid>? Exsudatos { get; set; }
}
