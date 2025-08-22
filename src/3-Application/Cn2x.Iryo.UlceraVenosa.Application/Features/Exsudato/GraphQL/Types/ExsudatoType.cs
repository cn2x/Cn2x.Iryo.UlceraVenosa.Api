using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Types;

public class ExsudatoType : ObjectType<Domain.Entities.Exsudato>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.Exsudato> descriptor)
    {
        descriptor.Name("Exsudato");
        descriptor.Description("Tipo de exsudato");
        descriptor.Field(x => x.Id).Description("Id do exsudato");
        descriptor.Field(x => x.Nome).Description("Nome do exsudato");
        descriptor.Field(x => x.Descricao).Description("Descrição do exsudato");
    }
} 