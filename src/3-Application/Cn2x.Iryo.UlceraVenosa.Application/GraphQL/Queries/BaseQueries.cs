using HotChocolate;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Domain.Models;
using MediatR;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Specifications;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

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
    public async Task<List<Paciente>> pacientes(
        string term,
        [Service] IMediator mediator, [Service] ApplicationDbContext ctx)
    {
        if (string.IsNullOrWhiteSpace(term))
            return new List<Paciente>();

        var query = new SearchPacienteQuery { Term = term };
        var result = await mediator.Send(query);
        return (result ?? Enumerable.Empty<Paciente>()).Take(100).ToList();
    }

    public async Task<List<Paciente>> paciente(
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
    public async Task<List<Exsudato>> exsudatos([
        Service] IRepository<Exsudato> repository)
    {
        var result = await repository.GetAllAsync();
        return result.ToList();
    }
}

[ExtendObjectType("Query")]
public class EnumeracoesQueries
{
    public IEnumerable<Clinica> clinicas() =>
        new[] { Clinica.C0, Clinica.C1, Clinica.C2, Clinica.C3, Clinica.C4a, Clinica.C4b, Clinica.C5, Clinica.C6 };

    public IEnumerable<Etiologica> etiologias() =>
        new[] { Etiologica.Ec, Etiologica.Ep, Etiologica.Es, Etiologica.En };

    public IEnumerable<Anatomica> anatomicas() =>
        new[] { Anatomica.As, Anatomica.Ad, Anatomica.Ap, Anatomica.An };

    public IEnumerable<Patofisiologica> patofisiologicas() =>
        new[] { Patofisiologica.Pr, Patofisiologica.Po, Patofisiologica.Pn };

    public IEnumerable<Lateralidade> lateralidades() =>
        new[] { Lateralidade.Direto, Lateralidade.Esquerdo };
}

[ExtendObjectType("Query")]
public class SegmentoQueries
{
    public async Task<List<Segmento>> segmentos([
        Service] IRepository<Segmento> repository)
    {
        var result = await repository.GetAllAsync();
        return result.ToList();
    }
}

[ExtendObjectType("Query")]
public class RegiaoAnatomicaQueries
{
    public async Task<List<RegiaoAnatomica>> regioesAnatomicasPorSegmento(
        Guid segmentoId,
        [Service] IRepository<RegiaoAnatomica> repository)
    {
        var spec = new RegiaoAnatomicaBySegmentoSpecification(segmentoId);
        var queryable = await repository.FindFilterByExpression<RegiaoAnatomica>(spec.SatisfiedBy());
        return queryable.ToList();
    }
} 