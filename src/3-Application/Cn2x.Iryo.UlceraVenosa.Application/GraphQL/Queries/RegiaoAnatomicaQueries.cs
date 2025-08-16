using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class RegiaoAnatomicaQueries
{
    /// <summary>
    /// Busca todas as regiões anatômicas da perna
    /// </summary>
    [GraphQLName("regioesAnatomicasPerna")]
    public async Task<List<RegiaoAnatomica>> GetRegioesAnatomicasPernaAsync([Service] ApplicationDbContext ctx)
    {
        throw new NotImplementedException();
    }
} 