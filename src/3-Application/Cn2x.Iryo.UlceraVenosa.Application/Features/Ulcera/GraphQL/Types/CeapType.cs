using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class CeapType : ObjectType<Ceap>
{
    protected override void Configure(IObjectTypeDescriptor<Ceap> descriptor)
    {
        descriptor.Name("Ceap");
        descriptor.Description("Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology)");

        descriptor.Field(x => x.ClasseClinica).Type<ClinicaType>().Description("Classe clínica");
        descriptor.Field(x => x.Etiologia).Type<EtiologicaType>().Description("Etiologia");
        descriptor.Field(x => x.Anatomia).Type<AnatomicaType>().Description("Anatomia");
        descriptor.Field(x => x.Patofisiologia).Type<PatofisiologicaType>().Description("Patofisiologia");
    }
} 