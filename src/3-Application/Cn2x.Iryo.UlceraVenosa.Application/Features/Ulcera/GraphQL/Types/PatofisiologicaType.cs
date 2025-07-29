using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class PatofisiologicaType : ObjectType<Patofisiologica>
{
    protected override void Configure(IObjectTypeDescriptor<Patofisiologica> descriptor)
    {
        descriptor.Name("Patofisiologica");
        descriptor.Description("Classificação patofisiológica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<PatofisiologicaEnum>>()
            .Resolve(ctx => ctx.Parent<Patofisiologica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação patofisiológica");
    }
} 