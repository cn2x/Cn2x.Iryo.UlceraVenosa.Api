using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters
{
    [ExcludeFromCodeCoverage]
    public class DorValueConverter : ValueConverter<Dor?, int>
    {
        public DorValueConverter() : base(
            dor => dor != null ? (int)dor.Intensidade : 0,
            valor => new Dor((Intensidade)valor)
        )
        {
        }
    }
}
