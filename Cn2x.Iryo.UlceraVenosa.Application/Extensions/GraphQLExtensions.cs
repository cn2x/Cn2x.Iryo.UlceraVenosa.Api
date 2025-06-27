using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Filters;

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
            .AddQueryType()
            .AddMutationType()
            .AddSubscriptionType()
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
            .AddQueryType()
            .AddMutationType()
            .AddSubscriptionType()
            .AddErrorFilter<GraphQLErrorFilter>()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
                opt.ExecutionTimeout = TimeSpan.FromSeconds(60);
            });

        return services;
    }
} 