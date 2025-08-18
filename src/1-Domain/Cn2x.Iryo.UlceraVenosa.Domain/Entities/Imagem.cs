using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Entidade que representa uma imagem armazenada no Google Cloud Storage.
/// </summary>
public class Imagem : Entity<Guid>, IAggregateRoot
{
    public string Url { get; private set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataCaptura { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    
    // Construtor para criação
    public Imagem(string url, string? descricao, DateTime dataCaptura)
    {
        Url = url ?? string.Empty;
        Descricao = descricao;
        DataCaptura = dataCaptura;
    }
    
    // Construtor para EF Core
    private Imagem() { }
    
    /// <summary>
    /// Atualiza a URL da imagem após upload para o Google Cloud Storage
    /// </summary>
    /// <param name="url">Nova URL da imagem</param>
    public void AtualizarUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL não pode ser vazia", nameof(url));
            
        Url = url;
    }
}
