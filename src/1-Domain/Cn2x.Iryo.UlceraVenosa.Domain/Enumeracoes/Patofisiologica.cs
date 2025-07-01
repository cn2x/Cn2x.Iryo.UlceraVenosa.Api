using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum PatofisiologicaEnum : int
{
    Pr = 1,
    Po = 2,
    Pn = 3
}

public class Patofisiologica : Enumeration<PatofisiologicaEnum>
{
    public static Patofisiologica Pr = new Patofisiologica(PatofisiologicaEnum.Pr, "Pr", "Refluxo");
    public static Patofisiologica Po = new Patofisiologica(PatofisiologicaEnum.Po, "Po", "Obstrução");
    public static Patofisiologica Pn = new Patofisiologica(PatofisiologicaEnum.Pn, "Pn", "Não identificada");

    public string Descricao { get; private set; }

    private Patofisiologica(PatofisiologicaEnum value, string displayName, string descricao)
        : base(value, displayName)
    {
        Descricao = descricao;
    }
} 