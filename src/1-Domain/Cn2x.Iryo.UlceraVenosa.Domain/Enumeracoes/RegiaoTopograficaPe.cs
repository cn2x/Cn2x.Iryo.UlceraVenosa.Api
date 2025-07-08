namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

public enum RegiaoTopograficaPeEnum
{
    Dorsal = 1,
    Plantar = 2,
    Calcaneo = 3,
    Mediopé = 4,
    Antepé = 5,
    Halux = 6,
    Lateral = 7,
    Medial = 8,
    MaleloMedial = 9,
    MaleloLateral = 10
}

public class RegiaoTopograficaPe : Enumeration<RegiaoTopograficaPeEnum>
{
    public static RegiaoTopograficaPe Dorsal = new(RegiaoTopograficaPeEnum.Dorsal, "DOR", "Dorsal");
    public static RegiaoTopograficaPe Plantar = new(RegiaoTopograficaPeEnum.Plantar, "PLA", "Plantar");
    public static RegiaoTopograficaPe Calcaneo = new(RegiaoTopograficaPeEnum.Calcaneo, "CAL", "Calcâneo");
    public static RegiaoTopograficaPe Mediopé = new(RegiaoTopograficaPeEnum.Mediopé, "MED", "Mediopé");
    public static RegiaoTopograficaPe Antepé = new(RegiaoTopograficaPeEnum.Antepé, "ANT", "Antepé");
    public static RegiaoTopograficaPe Halux = new(RegiaoTopograficaPeEnum.Halux, "HAL", "Halux");
    public static RegiaoTopograficaPe Lateral = new(RegiaoTopograficaPeEnum.Lateral, "LAT", "Lateral");
    public static RegiaoTopograficaPe Medial = new(RegiaoTopograficaPeEnum.Medial, "MEDL", "Medial");
    public static RegiaoTopograficaPe MaleloMedial = new(RegiaoTopograficaPeEnum.MaleloMedial, "MMED", "Malelo Medial");
    public static RegiaoTopograficaPe MaleloLateral = new(RegiaoTopograficaPeEnum.MaleloLateral, "MLAT", "Malelo Lateral");

    public string Descricao { get; private set; }

    protected RegiaoTopograficaPe() : base(default, string.Empty)
    {
        Descricao = string.Empty;
    }

    private RegiaoTopograficaPe(RegiaoTopograficaPeEnum value, string sigla, string descricao)
        : base(value, sigla)
    {
        Descricao = descricao;
    }
}
