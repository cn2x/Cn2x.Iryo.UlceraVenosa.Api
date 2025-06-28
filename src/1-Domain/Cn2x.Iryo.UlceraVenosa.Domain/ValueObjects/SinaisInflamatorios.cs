using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

/// <summary>
/// Value Object para sinais inflamatórios da úlcera
/// </summary>
public class SinaisInflamatorios : ValueObject
{
    public bool Eritema { get; set; }
    public bool Calor { get; set; }
    public bool Rubor { get; set; }
    public bool Edema { get; set; }
    public bool Dor { get; set; }
    public bool PerdadeFuncao { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Eritema;
        yield return Calor;
        yield return Rubor;
        yield return Edema;
        yield return Dor;
        yield return PerdadeFuncao;
    }
} 