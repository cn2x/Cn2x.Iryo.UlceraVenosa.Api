using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Imagem da úlcera para acompanhamento da evolução do tratamento
/// </summary>
public class ImagemUlcera : Entity<Guid>, IAggregateRoot
{
    public Guid UlceraId { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long TamanhoBytes { get; set; }
    public DateTime DataCaptura { get; set; }
    public string? Descricao { get; set; }
    public string? Observacoes { get; set; }
    public bool EhImagemPrincipal { get; set; } = false;
    public int OrdemExibicao { get; set; } = 0;
    
    // Navegação
    public virtual Ulcera? Ulcera { get; set; }
} 