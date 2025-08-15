using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;
using HotChocolate.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

public class UpsertAvaliacaoUlceraInput
{
    public Guid? Id { get; set; }
    public Guid UlceraId { get; set; }
    public Guid ProfissionalId { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public int MesesDuracao { get; set; }    
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