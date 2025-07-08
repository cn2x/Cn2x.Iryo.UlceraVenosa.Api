using System;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

/// <summary>
/// Value Object para metadados e validação de imagem.
/// </summary>
public class Imagem : ValueObject
{
    public TipoConteudo ContentType { get; private set; }
    public long TamanhoBytes { get; private set; }
    public DateTime DataCaptura { get; private set; }

    public const long TAMANHO_MAXIMO_BYTES = 5 * 1024 * 1024; // 5 MB

    public Imagem(TipoConteudo contentType, long tamanhoBytes, DateTime dataCaptura)
    {
        if (contentType is null)
            throw new ArgumentException("ContentType é obrigatório", nameof(contentType));
        if (contentType != TipoConteudo.Jpeg && contentType != TipoConteudo.Png)
            throw new ArgumentException("Tipo de conteúdo não suportado. Apenas JPEG e PNG são permitidos.", nameof(contentType));
        if (tamanhoBytes <= 0)
            throw new ArgumentException("Tamanho inválido", nameof(tamanhoBytes));
        if (tamanhoBytes > TAMANHO_MAXIMO_BYTES)
            throw new ArgumentException($"Tamanho máximo permitido é {TAMANHO_MAXIMO_BYTES / (1024 * 1024)} MB.", nameof(tamanhoBytes));
        ContentType = contentType;
        TamanhoBytes = tamanhoBytes;
        DataCaptura = dataCaptura;
    }

    // Construtor para EF Core
    private Imagem() {
        ContentType = TipoConteudo.Jpeg; // Valor default para EF
        DataCaptura = DateTime.MinValue;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentType;
        yield return TamanhoBytes;
        yield return DataCaptura;
    }
}
