using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum AnatomicaEnum : int {
    Superficial = 1,
    Profundo = 2,
    Perfurante = 3,
    NaoIdentificada = 4
}


public class Anatomica : Enumeration<AnatomicaEnum> {
    public static Anatomica Superficial = new(AnatomicaEnum.Superficial, "As", "Sistema superficial");
    public static Anatomica Profundo = new(AnatomicaEnum.Profundo, "Ad", "Sistema profundo");
    public static Anatomica Perfurante = new(AnatomicaEnum.Perfurante, "Ap", "Sistema perfurante");
    public static Anatomica NaoIdentificada = new(AnatomicaEnum.NaoIdentificada, "An", "Não identificada");

    public string Descricao { get; private set; }

    private Anatomica(AnatomicaEnum value, string sigla, string descricao)
        : base(value, sigla) {
        Descricao = descricao;
    }
}
