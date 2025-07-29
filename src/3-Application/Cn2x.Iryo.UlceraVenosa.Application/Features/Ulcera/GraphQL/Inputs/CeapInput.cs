using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Inputs;

public class CeapInput
{
    public ClinicaEnum ClasseClinica { get; set; }
    public EtiologicaEnum Etiologia { get; set; }
    public AnatomicaEnum Anatomia { get; set; }
    public PatofisiologicaEnum Patofisiologia { get; set; }
} 