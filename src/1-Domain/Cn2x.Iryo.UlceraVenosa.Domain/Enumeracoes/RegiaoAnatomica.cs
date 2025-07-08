using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum RegiaoAnatomicaEnum
{
    Medial = 1,
    Lateral = 2,
    Anterior = 3,
    Posterior = 4,
    AnteroMedial = 5,
    PosteroLateral = 6
}

public class RegiaoAnatomica : Enumeration<RegiaoAnatomicaEnum>
{
    public static RegiaoAnatomica Medial = new(RegiaoAnatomicaEnum.Medial, "M", "Medial");
    public static RegiaoAnatomica Lateral = new(RegiaoAnatomicaEnum.Lateral, "L", "Lateral");
    public static RegiaoAnatomica Anterior = new(RegiaoAnatomicaEnum.Anterior, "A", "Anterior");
    public static RegiaoAnatomica Posterior = new(RegiaoAnatomicaEnum.Posterior, "P", "Posterior");
    public static RegiaoAnatomica AnteroMedial = new(RegiaoAnatomicaEnum.AnteroMedial, "AM", "Anteromedial");
    public static RegiaoAnatomica PosteroLateral = new(RegiaoAnatomicaEnum.PosteroLateral, "PL", "Posterolateral");

    public string Descricao { get; private set; }

    protected RegiaoAnatomica() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private RegiaoAnatomica(RegiaoAnatomicaEnum value, string sigla, string descricao)
        : base(value, sigla)
    {
        Descricao = descricao;
    }
}
