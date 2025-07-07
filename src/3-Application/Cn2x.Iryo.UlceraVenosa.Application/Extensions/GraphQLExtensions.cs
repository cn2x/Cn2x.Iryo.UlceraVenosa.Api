using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Filters;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Mutations;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Types;

namespace Cn2x.Iryo.UlceraVenosa.Application.Extensions;

/// <summary>
/// Extensões para configuração do GraphQL
/// </summary>
public static class GraphQLExtensions
{
    /// <summary>
    /// Adiciona serviços GraphQL configurados
    /// </summary>
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<UlceraQueries>()
            .AddTypeExtension<PacienteQueries>()
            .AddTypeExtension<EnumeracoesQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<UlceraMutations>()
            .AddTypeExtension<MedidaMutations>()
            .AddTypeExtension<ExsudatoDaUlceraMutations>()
            .AddType<UlceraType>()
            .AddType<CaracteristicasType>()
            .AddType<SinaisInflamatoriosType>()
            .AddType<CeapType>()
            .AddType<ClinicaType>()
            .AddType<EtiologicaType>()
            .AddType<AnatomicaType>()
            .AddType<PatofisiologicaType>()
            .AddType<TopografiaType>()
            .AddType<ExsudatoDaUlceraType>()
            .AddType<ImagemUlceraType>()
            .AddType<PacienteType>()
            .AddType<PagedResultUlceraType>()
            .AddErrorFilter<GraphQLErrorFilter>()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = false;
                opt.ExecutionTimeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

    /// <summary>
    /// Configura o GraphQL para desenvolvimento
    /// </summary>
    public static IServiceCollection AddGraphQLServicesForDevelopment(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<UlceraQueries>()
            .AddTypeExtension<PacienteQueries>()
            .AddTypeExtension<EnumeracoesQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<UlceraMutations>()
            .AddTypeExtension<MedidaMutations>()
            .AddTypeExtension<ExsudatoDaUlceraMutations>()
            .AddType<UlceraType>()
            .AddType<CaracteristicasType>()
            .AddType<SinaisInflamatoriosType>()
            .AddType<CeapType>()
            .AddType<ClinicaType>()
            .AddType<EtiologicaType>()
            .AddType<AnatomicaType>()
            .AddType<PatofisiologicaType>()
            .AddType<TopografiaType>()
            .AddType<ExsudatoDaUlceraType>()
            .AddType<ImagemUlceraType>()
            .AddType<PacienteType>()
            .AddType<PagedResultUlceraType>()
            .AddErrorFilter<GraphQLErrorFilter>()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
                opt.ExecutionTimeout = TimeSpan.FromSeconds(60);
            });

        return services;
    }
} 