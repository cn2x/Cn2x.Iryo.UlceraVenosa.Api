using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.ValueConverters {
    [ExcludeFromCodeCoverage]
    public class LateralidadeValueConvert : ValueConverter<Lateralidade, LateralidadeEnum> {
        public LateralidadeValueConvert() : base(
            tipo => tipo.Id,
            valor => Enumeration<LateralidadeEnum>.FromValue<Lateralidade>(valor)
        ) {
        }
    }

}
