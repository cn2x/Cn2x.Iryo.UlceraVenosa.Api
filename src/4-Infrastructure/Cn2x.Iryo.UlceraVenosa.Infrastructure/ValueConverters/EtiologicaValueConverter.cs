using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters {
    [ExcludeFromCodeCoverage]
    public class EtiologicaValueConverter : ValueConverter<Etiologica, EtiologicaEnum> {
        public EtiologicaValueConverter() : base(
            tipo => tipo.Id,
            valor => Enumeration<EtiologicaEnum>.FromValue<Etiologica>(valor)
        ) {
        }
    }
} 