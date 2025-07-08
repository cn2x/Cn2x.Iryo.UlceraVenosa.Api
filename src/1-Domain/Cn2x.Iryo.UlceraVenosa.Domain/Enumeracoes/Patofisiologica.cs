using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum PatofisiologicaEnum : int {
    Refluxo = 1,
    Obstrucao = 2,
    NaoIdentificada = 3
}
public class Patofisiologica : Enumeration<PatofisiologicaEnum> {
    public static Patofisiologica Refluxo = new(PatofisiologicaEnum.Refluxo, "PR", "Refluxo venoso");
    public static Patofisiologica Obstrucao = new(PatofisiologicaEnum.Obstrucao, "PO", "Obstrução venosa");
    public static Patofisiologica NaoIdentificada = new(PatofisiologicaEnum.NaoIdentificada, "PN", "Patofisiologia não identificada");

    public string Descricao { get; private set; }

    // Construtor sem parâmetros para o Entity Framework
    protected Patofisiologica() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private Patofisiologica(PatofisiologicaEnum value, string sigla, string descricao)
        : base(value, sigla) {
        Descricao = descricao;
    }
}