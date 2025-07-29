using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class EtiologicaType : ObjectType<Etiologica>
{
    protected override void Configure(IObjectTypeDescriptor<Etiologica> descriptor)
    {
        descriptor.Name("Etiologica");
        descriptor.Description("Classificação etiológica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<EtiologicaEnum>>()
            .Resolve(ctx => ctx.Parent<Etiologica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação etiológica");
    }
} 