using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Types;

public class ExsudatoDaAvaliacaoType : ObjectType<ExsudatoDaAvaliacao>
{
    protected override void Configure(IObjectTypeDescriptor<ExsudatoDaAvaliacao> descriptor)
    {
        descriptor.Name("ExsudatoDaAvaliacao");
        descriptor.Description("Exsudato associado à avaliação");
        descriptor.Field(x => x.AvaliacaoUlceraId).Description("Id da avaliação");
        descriptor.Field(x => x.ExsudatoId).Description("Id do exsudato");
        descriptor.Field(x => x.Exsudato).Type<ExsudatoType>().Description("Exsudato");
    }
} 