using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Entidade que representa uma imagem armazenada no Google Cloud Storage.
/// </summary>
public class Imagem : Entity<Guid>, IAggregateRoot
{
    public string Url { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataCaptura { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    
    // Construtor para criação
    public Imagem(string url, string? descricao, DateTime dataCaptura)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL é obrigatória", nameof(url));
            
        Url = url;
        Descricao = descricao;
        DataCaptura = dataCaptura;
    }
    
    // Construtor para EF Core
    private Imagem() { }
}
