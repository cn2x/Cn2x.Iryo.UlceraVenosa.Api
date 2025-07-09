using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Specifications;

namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;

/// <summary>
/// Classe base para queries GraphQL
/// </summary>
public abstract class BaseQueries
{
    /// <summary>
    /// Obtém todas as entidades paginadas
    /// </summary>
    protected async Task<PagedResult<T>> GetPagedAsync<T>(
        IRepository<T> repository,
        int page = 1,
        int pageSize = 10) where T : class, IAggregateRoot
    {
        // Implementação será adicionada quando os repositórios específicos forem criados
        throw new NotImplementedException("Método será implementado nos repositórios específicos");
    }

    /// <summary>
    /// Obtém todas as entidades
    /// </summary>
    protected async Task<IEnumerable<T>> GetAllAsync<T>(
        IRepository<T> repository) where T : class, IAggregateRoot
    {
        return await repository.GetAllAsync();
    }

    /// <summary>
    /// Obtém uma entidade por ID
    /// </summary>
    protected async Task<T?> GetByIdAsync<T>(
        Guid id,
        IRepository<T> repository) where T : class, IAggregateRoot
    {
        return await repository.GetByIdAsync(id);
    }
}

[ExtendObjectType("Query")]
public class PacienteQueries
{
    [GraphQLName("pacientes")]
    public async Task<List<Paciente>> Pacientes(
        string term,
        [Service] IMediator mediator, [Service] ApplicationDbContext ctx)
    {
        if (string.IsNullOrWhiteSpace(term))
            return new List<Paciente>();

        var query = new SearchPacienteQuery { Term = term };
        var result = await mediator.Send(query);
        return (result ?? Enumerable.Empty<Paciente>()).Take(100).ToList();
    }

    [GraphQLName("paciente")]
    public async Task<List<Paciente>> Paciente(
        Guid? id,
        string? cpf,
        string? nome,
        [Service] IRepository<Paciente> repository)
    {
        var spec = new PacienteOrSpecification(id, cpf, nome);
        var queryable = await repository.FindFilterByExpression<Paciente>(spec.SatisfiedBy());
        return queryable.ToList();
    }
}

[ExtendObjectType("Query")]
public class ExsudatoQueries
{
    [GraphQLName("exsudatos")]
    public async Task<List<Exsudato>> Exsudatos([
        Service] IRepository<Exsudato> repository)
    {
        var result = await repository.GetAllAsync();
        return result.ToList();
    }
}

// Removido: EnumeracoesQueries e métodos baseados em Enumeration<T> e enums antigos.
// As queries de Segmento e RegiaoAnatomica foram removidas pois não fazem mais parte da modelagem.
// Caso precise de queries para enums ou value objects, implemente conforme a nova estrutura.
// public class SegmentoQueries { ... }
// public class RegiaoAnatomicaQueries { ... }