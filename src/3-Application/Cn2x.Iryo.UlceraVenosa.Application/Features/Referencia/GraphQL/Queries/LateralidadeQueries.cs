using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Queries;

[ExtendObjectType("Query")]
public class LateralidadeQueries
{
    [GraphQLName("lateralidades")]
    public async Task<List<Lateralidade>> GetLateralidadesAsync([Service] IRepository<Lateralidade> repository)
    {
        var lateralidades = await repository.GetAllAsync();
        return lateralidades.ToList();
    }
} 