namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public abstract class Telefone: Contato
{
    protected bool IsValid(string number)
    {
        if (number.Length != 13) return false;
        
        var ddi = number.Substring(0, 2);
        if (!IsDdiValid(ddi)) return false;
        
        var ddd = number.Substring(2, 2);
        if (!IsDddValid(ddd)) return false;

        char nineObrigatory = number[4];
        if (number.Length == 13 && nineObrigatory == '9') return false;

        return true;
    }

    private bool IsDddValid(string ddd)
    {
        return Enum.TryParse(ddd, out DDD result);
    }

    private bool IsDdiValid(string ddi)
    {
        return Enum.TryParse(ddi, out DDI result);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}