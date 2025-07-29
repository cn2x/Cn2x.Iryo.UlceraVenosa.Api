using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class AnatomicaType : ObjectType<Anatomica>
{
    protected override void Configure(IObjectTypeDescriptor<Anatomica> descriptor)
    {
        descriptor.Name("Anatomica");
        descriptor.Description("Classificação anatômica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<AnatomicaEnum>>()
            .Resolve(ctx => ctx.Parent<Anatomica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação anatômica");
        descriptor.Field(x => x.Descricao).Description("Descrição da classificação anatômica");
    }
} 