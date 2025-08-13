using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Inputs;

public class ImagemInput
{
    // Base64 opcional (para compatibilidade)
    public string? ArquivoBase64 { get; set; }
    
    // Metadados obrigatórios
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCaptura { get; set; }
    
    // Metadados opcionais
    public string? NomeArquivo { get; set; }
    public string? TipoConteudo { get; set; }
    public long? TamanhoBytes { get; set; }
}
