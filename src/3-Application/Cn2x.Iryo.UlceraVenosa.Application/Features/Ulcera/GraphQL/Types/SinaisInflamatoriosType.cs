using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class SinaisInflamatoriosType : ObjectType<SinaisInflamatorios>
{
    protected override void Configure(IObjectTypeDescriptor<SinaisInflamatorios> descriptor)
    {
        descriptor.Name("SinaisInflamatorios");
        descriptor.Description("Sinais inflamatórios da úlcera");

        descriptor.Field(x => x.Dor).Description("Presença de dor");
        descriptor.Field(x => x.Calor).Description("Presença de calor");
        descriptor.Field(x => x.Rubor).Description("Presença de rubor");
        descriptor.Field(x => x.Edema).Description("Presença de edema");
    }
} 