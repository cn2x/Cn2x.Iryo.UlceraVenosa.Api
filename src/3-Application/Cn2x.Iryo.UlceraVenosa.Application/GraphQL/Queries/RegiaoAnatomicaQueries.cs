using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class RegiaoAnatomicaQueries
{
    /// <summary>
    /// Busca todas as regiões anatômicas da perna
    /// </summary>
    [GraphQLName("regioesAnatomicasPerna")]
    public async Task<List<RegiaoAnatomica>> GetRegioesAnatomicasPernaAsync([Service] IRepository<RegiaoAnatomica> repository)
    {
        var regioes = await repository.GetAllAsync();
        return regioes.ToList();
    }
} 