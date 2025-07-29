using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Types;

public class MedidaType : ObjectType<Domain.ValueObjects.Medida>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.ValueObjects.Medida> descriptor)
    {
        descriptor.Name("Medida");
        descriptor.Description("Medidas da úlcera (relacionamento 1:1)");
        descriptor.Field(x => x.Comprimento).Description("Comprimento em cm");
        descriptor.Field(x => x.Largura).Description("Largura em cm");
        descriptor.Field(x => x.Profundidade).Description("Profundidade em cm");
    }
} 