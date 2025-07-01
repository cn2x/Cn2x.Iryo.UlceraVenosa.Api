using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum EtiologicaEnum : int
{
    Ec = 1,
    Ep = 2,
    Es = 3,
    En = 4
}

public class Etiologica : Enumeration<EtiologicaEnum>
{
    public static Etiologica Ec = new Etiologica(EtiologicaEnum.Ec, "Ec", "Congênita");
    public static Etiologica Ep = new Etiologica(EtiologicaEnum.Ep, "Ep", "Primária");
    public static Etiologica Es = new Etiologica(EtiologicaEnum.Es, "Es", "Secundária (pós-trombótica)");
    public static Etiologica En = new Etiologica(EtiologicaEnum.En, "En", "Não identificada");

    public string Descricao { get; private set; }

    private Etiologica(EtiologicaEnum value, string displayName, string descricao)
        : base(value, displayName)
    {
        Descricao = descricao;
    }
} 