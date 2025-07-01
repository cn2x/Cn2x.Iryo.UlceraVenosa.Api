using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum AnatomicaEnum : int
{
    As = 1,
    Ad = 2,
    Ap = 3,
    An = 4
}

public class Anatomica : Enumeration<AnatomicaEnum>
{
    public static Anatomica As = new Anatomica(AnatomicaEnum.As, "As", "Sistema superficial");
    public static Anatomica Ad = new Anatomica(AnatomicaEnum.Ad, "Ad", "Sistema profundo");
    public static Anatomica Ap = new Anatomica(AnatomicaEnum.Ap, "Ap", "Sistema perfurante");
    public static Anatomica An = new Anatomica(AnatomicaEnum.An, "An", "Não identificada");

    public string Descricao { get; private set; }

    private Anatomica(AnatomicaEnum value, string displayName, string descricao)
        : base(value, displayName)
    {
        Descricao = descricao;
    }
} 