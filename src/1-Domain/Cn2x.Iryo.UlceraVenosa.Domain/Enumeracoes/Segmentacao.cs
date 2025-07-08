using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum SegmentoEnum
{
    TercoSuperior = 1,
    TercoMedio = 2,
    TercoInferior = 3
}

public class Segmentacao : Enumeration<SegmentoEnum>
{
    public static Segmentacao TercoSuperior = new(SegmentoEnum.TercoSuperior, "TS", "Da fossa poplítea até ~2/3 da altura da perna");
    public static Segmentacao TercoMedio = new(SegmentoEnum.TercoMedio, "TM", "Da porção média até cerca de 1/3 acima do maléolo");
    public static Segmentacao TercoInferior = new(SegmentoEnum.TercoInferior, "TI", "Do final do médio até os maléolos (região do tornozelo)");

    public string Descricao { get; private set; }

    protected Segmentacao() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private Segmentacao(SegmentoEnum value, string sigla, string descricao)
        : base(value, sigla)
    {
        Descricao = descricao;
    }
}
