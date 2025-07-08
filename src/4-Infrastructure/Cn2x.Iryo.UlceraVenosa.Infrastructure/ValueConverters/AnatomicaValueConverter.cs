using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters {
    [ExcludeFromCodeCoverage]
    public class AnatomicaValueConverter : ValueConverter<Anatomica, AnatomicaEnum> {
        public AnatomicaValueConverter() : base(
            tipo => tipo.Id,
            valor => Enumeration<AnatomicaEnum>.FromValue<Anatomica>(valor)
        ) {
        }
    }
} 