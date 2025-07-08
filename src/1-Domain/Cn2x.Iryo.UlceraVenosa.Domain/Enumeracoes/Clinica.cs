using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum ClinicaEnum : int {
    SemSinais = 1,                      // C0
    Telangiectasias = 2,               // C1
    Varizes = 3,                        // C2
    Edema = 4,                          // C3
    PigmentacaoOuEczema = 5,           // C4a
    LipodermatoescleroseOuAtrofia = 6, // C4b
    UlceraCicatrizada = 7,             // C5
    UlceraAtiva = 8                    // C6
}


public class Clinica : Enumeration<ClinicaEnum> {
    public static Clinica SemSinais = new(ClinicaEnum.SemSinais, "C0", "Sem sinais visíveis ou palpáveis de doença venosa");
    public static Clinica Telangiectasias = new(ClinicaEnum.Telangiectasias, "C1", "Telangiectasias ou veias reticulares");
    public static Clinica Varizes = new(ClinicaEnum.Varizes, "C2", "Veias varicosas");
    public static Clinica Edema = new(ClinicaEnum.Edema, "C3", "Edema");
    public static Clinica PigmentacaoOuEczema = new(ClinicaEnum.PigmentacaoOuEczema, "C4a", "Pigmentação ou eczema");
    public static Clinica LipodermatoescleroseOuAtrofia = new(ClinicaEnum.LipodermatoescleroseOuAtrofia, "C4b", "Lipodermatoesclerose ou atrofia branca");
    public static Clinica UlceraCicatrizada = new(ClinicaEnum.UlceraCicatrizada, "C5", "Úlcera venosa cicatrizada");
    public static Clinica UlceraAtiva = new(ClinicaEnum.UlceraAtiva, "C6", "Úlcera venosa ativa");

    public string Descricao { get; private set; }

    // Construtor sem parâmetros para o Entity Framework
    protected Clinica() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private Clinica(ClinicaEnum value, string displayName, string descricao)
        : base(value, displayName) {
        Descricao = descricao;
    }
}
