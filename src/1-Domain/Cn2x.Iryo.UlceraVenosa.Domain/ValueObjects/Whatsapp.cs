namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public class Whatsapp: Telefone
{
    private string Number { get; init; } = String.Empty;

    public string GetNumber()
    {
        return Number;
    }

    public Whatsapp(string number)
    { 
        if(IsValid(number)) Number = number;
    }
}