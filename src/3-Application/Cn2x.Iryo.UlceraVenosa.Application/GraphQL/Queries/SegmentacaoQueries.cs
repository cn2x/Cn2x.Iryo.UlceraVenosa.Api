using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

[ExtendObjectType("Query")]
public class SegmentacaoQueries
{
    /// <summary>
    /// Busca todas as segmentações disponíveis
    /// </summary>
    [GraphQLName("segmentacoes")]
    public async Task<List<Segmentacao>> GetSegmentacoesAsync([Service] IRepository<Segmentacao> repository)
    {
        var segmentacoes = await repository.GetAllAsync();
        return segmentacoes.ToList();
    }
} 