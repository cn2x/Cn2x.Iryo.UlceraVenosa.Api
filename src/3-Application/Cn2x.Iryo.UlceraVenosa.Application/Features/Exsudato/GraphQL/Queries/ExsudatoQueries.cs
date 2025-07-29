using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Queries;

[ExtendObjectType("Query")]
public class ExsudatoQueries
{
    [GraphQLName("exsudatos")]
    public async Task<List<Domain.Entities.Exsudato>> Exsudatos([Service] ApplicationDbContext context)
    {
        var result = await context.GetExsudatosAtivosAsync();
        return result.ToList();
    }
} 