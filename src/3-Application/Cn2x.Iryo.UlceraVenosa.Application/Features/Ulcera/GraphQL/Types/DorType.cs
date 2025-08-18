using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class DorType : ObjectType<Dor>
{
    protected override void Configure(IObjectTypeDescriptor<Dor> descriptor)
    {
        descriptor.Name("Dor");
        descriptor.Description("Informações de dor");
        descriptor.Field(x => x.Intensidade).Type<NonNullType<IntensidadeType>>();
    }
}

public class DorInputType : InputObjectType
{
    protected override void Configure(IInputObjectTypeDescriptor descriptor)
    {
        descriptor.Name("DorInput");
        descriptor.Description("Input para dor");
        descriptor.Field("intensidade").Type<NonNullType<IntensidadeType>>();
    }
}

public class IntensidadeType : EnumType<Intensidade>
{
    protected override void Configure(IEnumTypeDescriptor<Intensidade> descriptor)
    {
        descriptor.Name("Intensidade");
    }
}


