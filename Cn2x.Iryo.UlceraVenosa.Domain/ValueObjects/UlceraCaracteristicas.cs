using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

/// <summary>
/// Value Object para características da úlcera
/// </summary>
public class UlceraCaracteristicas : ValueObject
{
    public bool BordasDefinidas { get; set; }
    public bool TecidoGranulacao { get; set; }
    public bool Necrose { get; set; }
    public bool OdorFetido { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return BordasDefinidas;
        yield return TecidoGranulacao;
        yield return Necrose;
        yield return OdorFetido;
    }
} 