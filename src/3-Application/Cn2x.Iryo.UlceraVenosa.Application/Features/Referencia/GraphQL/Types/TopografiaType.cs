using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Types;

public interface ITopografiaType {}

public class TopografiaInterfaceType : InterfaceType<Topografia>
{
    protected override void Configure(IInterfaceTypeDescriptor<Topografia> descriptor)
    {
        descriptor.Name("Topografia");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.ResolveAbstractType((ctx, obj) =>
        {
            if (obj is TopografiaPerna) return ctx.Schema.GetType<TopografiaPernaType>(nameof(TopografiaPerna));
            if (obj is TopografiaPe) return ctx.Schema.GetType<TopografiaPeType>(nameof(TopografiaPe));
            return null;
        });
    }
}

public class TopografiaPernaType : ObjectType<TopografiaPerna>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPerna> descriptor)
    {
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Name("TopografiaPerna");
        descriptor.Description("Topografia anatômica da perna");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.Segmentacao).Description("Segmentação");
        descriptor.Field(x => x.RegiaoAnatomica).Description("Região anatômica");
    }
}

public class TopografiaPeType : ObjectType<TopografiaPe>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPe> descriptor)
    {
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Name("TopografiaPe");
        descriptor.Description("Topografia anatômica do pé");
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.RegiaoTopograficaPe).Description("Região topográfica do pé");
    }
} 