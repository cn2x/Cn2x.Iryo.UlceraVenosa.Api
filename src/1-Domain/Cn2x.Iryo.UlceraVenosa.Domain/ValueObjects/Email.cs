using System.Text.RegularExpressions;
using static System.String;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public partial class Email: Contato
{
    private string Address { get; }

    public string GetAddress()
    {
        return Address; 
    }

    public Email(string address)
    {
        Address = IsValid(address) ? address : Empty;
    }   

    private static bool IsValid(string address) => EmailRegex.IsMatch(address);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
}