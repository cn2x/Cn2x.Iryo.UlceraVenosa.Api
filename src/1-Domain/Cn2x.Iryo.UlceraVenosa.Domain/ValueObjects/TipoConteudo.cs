using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public class TipoConteudo : Enumeration<string> {
    public static readonly TipoConteudo Jpeg = new("image/jpeg", "JPEG");
    public static readonly TipoConteudo Png = new("image/png", "PNG");    

    public string MimeType { get; }

    private TipoConteudo(string value, string displayName) : base(value, displayName) {
        MimeType = value;
    }
}
