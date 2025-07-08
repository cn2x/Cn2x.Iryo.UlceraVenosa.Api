using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters;

public class TipoConteudoValueConverter : ValueConverter<TipoConteudo, string>
{
    public TipoConteudoValueConverter() : base(
        v => v.MimeType,
        v => v == TipoConteudo.Jpeg.MimeType ? TipoConteudo.Jpeg : TipoConteudo.Png)
    {
    }
}
