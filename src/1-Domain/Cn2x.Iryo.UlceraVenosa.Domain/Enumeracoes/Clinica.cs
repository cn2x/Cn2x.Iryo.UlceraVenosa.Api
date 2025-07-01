using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum ClinicaEnum : int
{
    C0 = 1,
    C1 = 2,
    C2 = 3,
    C3 = 4,
    C4a = 5,
    C4b = 6,
    C5 = 7,
    C6 = 8
}

public class Clinica : Enumeration<ClinicaEnum>
{
    public static Clinica C0 = new Clinica(ClinicaEnum.C0, "C0", "Sem sinais visíveis ou palpáveis de doença venosa");
    public static Clinica C1 = new Clinica(ClinicaEnum.C1, "C1", "Telangiectasias ou veias reticulares");
    public static Clinica C2 = new Clinica(ClinicaEnum.C2, "C2", "Veias varicosas");
    public static Clinica C3 = new Clinica(ClinicaEnum.C3, "C3", "Edema");
    public static Clinica C4a = new Clinica(ClinicaEnum.C4a, "C4a", "Pigmentação ou eczema");
    public static Clinica C4b = new Clinica(ClinicaEnum.C4b, "C4b", "Lipodermatoesclerose ou atrofia branca");
    public static Clinica C5 = new Clinica(ClinicaEnum.C5, "C5", "Úlcera venosa cicatrizada");
    public static Clinica C6 = new Clinica(ClinicaEnum.C6, "C6", "Úlcera venosa ativa");

    public string Descricao { get; private set; }

    private Clinica(ClinicaEnum value, string displayName, string descricao)
        : base(value, displayName)
    {
        Descricao = descricao;
    }
} 