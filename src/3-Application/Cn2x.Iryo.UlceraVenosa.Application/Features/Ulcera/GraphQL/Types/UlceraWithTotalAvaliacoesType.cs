using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;

public class UlceraWithTotalAvaliacoesType : ObjectType<UlceraWithTotalAvaliacoes>
{
    protected override void Configure(IObjectTypeDescriptor<UlceraWithTotalAvaliacoes> descriptor)
    {
        descriptor.Name("UlceraWithTotalAvaliacoes");
        descriptor.Description("Úlcera com total de avaliações");
        
        descriptor.Field(x => x.Ulcera).Description("Dados da úlcera");
        descriptor.Field(x => x.TotalAvaliacoes).Description("Total de avaliações da úlcera");
    }
}
