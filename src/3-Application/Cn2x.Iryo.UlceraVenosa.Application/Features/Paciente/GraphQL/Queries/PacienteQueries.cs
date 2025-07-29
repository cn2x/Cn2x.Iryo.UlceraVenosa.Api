using HotChocolate;
using HotChocolate.Types;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente.GraphQL.Queries;

[ExtendObjectType("Query")]
public class PacienteQueries
{
    [GraphQLName("pacientes")]
    public async Task<List<Domain.Entities.Paciente>> Pacientes(
        string term,
        [Service] ApplicationDbContext context)
    {
        var result = await context.SearchPacientesAsync(term);
        return result.ToList();
    }

    [GraphQLName("paciente")]
    public async Task<List<Domain.Entities.Paciente>> Paciente(
        Guid? id,
        string? cpf,
        string? nome,
        [Service] ApplicationDbContext context)
    {
        return await context.GetPacientesAsync(id, cpf, nome);
    }
} 