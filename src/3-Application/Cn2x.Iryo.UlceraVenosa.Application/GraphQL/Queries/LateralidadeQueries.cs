using HotChocolate;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class LateralidadeQueries
{
    [GraphQLName("lateralidades")]
    public async Task<List<Lateralidade>> GetLateralidadesAsync([Service] ApplicationDbContext ctx)
    {
        throw new NotImplementedException();
    }
} 