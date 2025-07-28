using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class SegmentacaoQueries
{
    /// <summary>
    /// Busca todas as segmentações disponíveis
    /// </summary>
    [GraphQLName("segmentacoes")]
    public async Task<List<Segmentacao>> GetSegmentacoesAsync([Service] ApplicationDbContext ctx)
    {
        return await ctx.GetSegmentacoesAtivasAsync();
    }
} 