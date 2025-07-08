using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum EtiologicaEnum : int {
    Congenita = 1,
    Primaria = 2,
    Secundaria = 3,
    NaoIdentificada = 4
}

public class Etiologica : Enumeration<EtiologicaEnum> {
    public static Etiologica Congenita = new(EtiologicaEnum.Congenita, "Ec", "Congênita");
    public static Etiologica Primaria = new(EtiologicaEnum.Primaria, "Ep", "Primária");
    public static Etiologica Secundaria = new(EtiologicaEnum.Secundaria, "Es", "Secundária (pós-trombótica)");
    public static Etiologica NaoIdentificada = new(EtiologicaEnum.NaoIdentificada, "En", "Não identificada");

    public string Descricao { get; private set; }

    // Construtor sem parâmetros para o Entity Framework
    protected Etiologica() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private Etiologica(EtiologicaEnum value, string sigla, string descricao)
        : base(value, sigla) {
        Descricao = descricao;
    }
}
