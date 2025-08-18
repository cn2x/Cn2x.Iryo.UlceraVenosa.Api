using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class SinaisInflamatoriosType : ObjectType<SinaisInflamatorios>
{
    protected override void Configure(IObjectTypeDescriptor<SinaisInflamatorios> descriptor)
    {
        descriptor.Name("SinaisInflamatorios");
        descriptor.Description("Sinais inflamatórios da úlcera");

        descriptor.Field(x => x.Calor).Description("Presença de calor");
        descriptor.Field(x => x.Rubor).Description("Presença de rubor");
        descriptor.Field(x => x.Edema).Description("Presença de edema");
        descriptor.Field(x => x.Dor).Type<DorType>().Description("Presença de dor");
        descriptor.Field(x => x.PerdadeFuncao).Description("Perda de função");
        descriptor.Field(x => x.Eritema).Description("Presença de eritema");
    }
}

public class SinaisInflamatoriosInputType : InputObjectType<SinaisInflamatorios>
{
    protected override void Configure(IInputObjectTypeDescriptor<SinaisInflamatorios> descriptor)
    {
        descriptor.Name("SinaisInflamatoriosInput");
        descriptor.Description("Input para sinais inflamatórios");
        descriptor.BindFieldsExplicitly();
        
        descriptor.Field("calor").Type<BooleanType>();
        descriptor.Field("rubor").Type<BooleanType>();
        descriptor.Field("edema").Type<BooleanType>();
        descriptor.Field("dor").Type<IntensidadeType>();
        descriptor.Field("perdadeFuncao").Type<BooleanType>();
        descriptor.Field("eritema").Type<BooleanType>();
    }
} 