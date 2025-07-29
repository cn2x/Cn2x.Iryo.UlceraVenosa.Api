using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using HotChocolate.Types.Relay;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Types;

public class LateralidadeType : ObjectType<Lateralidade>
{
    protected override void Configure(IObjectTypeDescriptor<Lateralidade> descriptor)
    {
        descriptor.Name("Lateralidade");
        descriptor.Description("Lateralidade da topografia");

        descriptor.Field(x => x.Id)
            .Type<UuidType>()
            .Description("Id da lateralidade");
        descriptor.Field(x => x.Nome).Description("Nome da lateralidade");
    }
} 