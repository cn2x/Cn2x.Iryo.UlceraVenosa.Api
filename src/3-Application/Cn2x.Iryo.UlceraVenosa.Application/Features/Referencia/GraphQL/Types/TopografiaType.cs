using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Types;

public class TopografiaInterfaceType : InterfaceType<Topografia>
{
    protected override void Configure(IInterfaceTypeDescriptor<Topografia> descriptor)
    {
        descriptor.Name("Topografia");
        descriptor.Field(x => x.Id).Description("Id da topografia");
    }
}

public class TopografiaPernaType : ObjectType<TopografiaPerna>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPerna> descriptor)
    {
        descriptor.Name("TopografiaPerna");
        descriptor.Description("Topografia anatômica da perna");
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.LateralidadeId).Description("Id da lateralidade");
        descriptor.Field(x => x.SegmentacaoId).Description("Id da segmentação");
        descriptor.Field(x => x.RegiaoAnatomicaId).Description("Id da região anatômica");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.Segmentacao).Description("Segmentação");
        descriptor.Field(x => x.RegiaoAnatomica).Description("Região anatômica");
    }
}

public class TopografiaPeType : ObjectType<TopografiaPe>
{
    protected override void Configure(IObjectTypeDescriptor<TopografiaPe> descriptor)
    {
        descriptor.Name("TopografiaPe");
        descriptor.Description("Topografia anatômica do pé");
        descriptor.Implements<TopografiaInterfaceType>();
        descriptor.Field(x => x.Id).Description("Id da topografia");
        descriptor.Field(x => x.LateralidadeId).Description("Id da lateralidade");
        descriptor.Field(x => x.RegiaoTopograficaPeId).Description("Id da região topográfica do pé");
        descriptor.Field(x => x.Lateralidade).Description("Lateralidade");
        descriptor.Field(x => x.RegiaoTopograficaPe).Description("Região topográfica do pé");
    }
} 