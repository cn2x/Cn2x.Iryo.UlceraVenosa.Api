using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Filters;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Mutations;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Mutations;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Mutations;
// Types organizados por feature
using Cn2x.Iryo.UlceraVenosa.Application.Features.Ulcera.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Paciente.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Medida.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Referencia.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.GraphQL.Types;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Types;

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
            .AddTypeExtension<ClassificacaoQueries>()
            .AddTypeExtension<PacienteQueries>()
            .AddTypeExtension<SegmentacaoQueries>()
            .AddTypeExtension<RegiaoAnatomicaQueries>()
            .AddTypeExtension<LateralidadeQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<UlceraMutations>()
            .AddTypeExtension<UlceraPernaMutations>()
            .AddTypeExtension<UlceraPeMutations>()
            .AddTypeExtension<MedidaMutations>()
            // Types da feature Ulcera
            .AddType<UlceraType>()
            .AddType<CaracteristicasType>()
            .AddType<SinaisInflamatoriosType>()
            .AddType<CeapType>()
            .AddType<ClinicaType>()
            .AddType<EtiologicaType>()
            .AddType<AnatomicaType>()
            .AddType<PatofisiologicaType>()
            .AddType<PagedResultUlceraType>()
            // Types da feature Paciente
            .AddType<PacienteType>()
            // Types da feature Medida
            .AddType<MedidaType>()
            // Types da feature Referencia
            .AddType<TopografiaInterfaceType>()
            .AddType<TopografiaPernaType>()
            .AddType<TopografiaPeType>()
            .AddType<LateralidadeType>()
            // Types da feature AvaliacaoUlcera
            .AddType<AvaliacaoUlceraType>()
            .AddType<ImagemAvaliacaoUlceraType>()
            .AddType<ExsudatoDaAvaliacaoType>()
            .AddType<ExsudatoType>()
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
            .AddTypeExtension<ClassificacaoQueries>()
            .AddTypeExtension<PacienteQueries>()
            .AddTypeExtension<SegmentacaoQueries>()
            .AddTypeExtension<RegiaoAnatomicaQueries>()
            .AddTypeExtension<LateralidadeQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<UlceraMutations>()
            .AddTypeExtension<UlceraPernaMutations>()
            .AddTypeExtension<UlceraPeMutations>()
            .AddTypeExtension<MedidaMutations>()
            // Types da feature Ulcera
            .AddType<UlceraType>()
            .AddType<CaracteristicasType>()
            .AddType<SinaisInflamatoriosType>()
            .AddType<CeapType>()
            .AddType<ClinicaType>()
            .AddType<EtiologicaType>()
            .AddType<AnatomicaType>()
            .AddType<PatofisiologicaType>()
            .AddType<PagedResultUlceraType>()
            // Types da feature Paciente
            .AddType<PacienteType>()
            // Types da feature Medida
            .AddType<MedidaType>()
            // Types da feature Referencia
            .AddType<TopografiaInterfaceType>()
            .AddType<TopografiaPernaType>()
            .AddType<TopografiaPeType>()
            .AddType<LateralidadeType>()
            // Types da feature AvaliacaoUlcera
            .AddType<AvaliacaoUlceraType>()
            .AddType<ImagemAvaliacaoUlceraType>()
            .AddType<ExsudatoDaAvaliacaoType>()
            .AddType<ExsudatoType>()
            .AddErrorFilter<GraphQLErrorFilter>()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
                opt.ExecutionTimeout = TimeSpan.FromSeconds(60);
            });

        return services;
    }
}