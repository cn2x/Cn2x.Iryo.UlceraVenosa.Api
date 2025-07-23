using System;
using System.Collections.Generic;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public class Medida : ValueObject
{
    public decimal? Comprimento { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Profundidade { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Comprimento;
        yield return Largura;
        yield return Profundidade;
    }
}