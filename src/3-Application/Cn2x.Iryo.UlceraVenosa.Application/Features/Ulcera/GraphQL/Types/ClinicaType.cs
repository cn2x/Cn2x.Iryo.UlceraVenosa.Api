using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class ClinicaType : ObjectType<Clinica>
{
    protected override void Configure(IObjectTypeDescriptor<Clinica> descriptor)
    {
        descriptor.Name("Clinica");
        descriptor.Description("Classificação clínica da úlcera");

        descriptor.Field("id")
            .Type<EnumType<ClinicaEnum>>()
            .Resolve(ctx => ctx.Parent<Clinica>().Id);
        descriptor.Field(x => x.Name).Description("Nome da classificação clínica");
    }
} 