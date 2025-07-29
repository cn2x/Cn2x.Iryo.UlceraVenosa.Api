using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Cn2x.Iryo.UlceraVenosa.Application.GraphQL.Filters;

/// <summary>
/// Filtro para tratamento de erros GraphQL
/// </summary>
public class GraphQLErrorFilter : IErrorFilter
{
    private readonly ILogger<GraphQLErrorFilter> _logger;

    public GraphQLErrorFilter(ILogger<GraphQLErrorFilter> logger)
    {
        _logger = logger;
    }

    public IError OnError(IError error)
    {
        _logger.LogError(error.Exception, "GraphQL Error: {Message}", error.Message);

        // Tratamento específico para diferentes tipos de erro
        return error.Exception switch
        {
            DbUpdateException dbException => HandleDatabaseException(dbException, error),
            ArgumentException argException => HandleArgumentException(argException, error),
            InvalidOperationException invException => HandleInvalidOperationException(invException, error),
            _ => HandleGenericException(error)
        };
    }

    private IError HandleDatabaseException(DbUpdateException exception, IError originalError)
    {
        var message = exception.InnerException?.Message ?? exception.Message;

        if (message.Contains("duplicate key"))
        {
            return ErrorBuilder.New()
                .SetMessage("Registro duplicado encontrado.")
                .SetCode("DUPLICATE_KEY")
                .SetPath(originalError.Path)
                .Build();
        }

        if (message.Contains("foreign key"))
        {
            return ErrorBuilder.New()
                .SetMessage("Referência inválida. Verifique os dados relacionados.")
                .SetCode("FOREIGN_KEY_VIOLATION")
                .SetPath(originalError.Path)
                .Build();
        }

        return ErrorBuilder.New()
            .SetMessage("Erro no banco de dados. Tente novamente.")
            .SetCode("DATABASE_ERROR")
            .SetPath(originalError.Path)
            .Build();
    }

    private IError HandleArgumentException(ArgumentException exception, IError originalError)
    {
        return ErrorBuilder.New()
            .SetMessage($"Argumento inválido: {exception.Message}")
            .SetCode("INVALID_ARGUMENT")
            .SetPath(originalError.Path)
            .Build();
    }

    private IError HandleInvalidOperationException(InvalidOperationException exception, IError originalError)
    {
        return ErrorBuilder.New()
            .SetMessage($"Operação inválida: {exception.Message}")
            .SetCode("INVALID_OPERATION")
            .SetPath(originalError.Path)
            .Build();
    }

    private IError HandleGenericException(IError originalError)
    {
        return ErrorBuilder.New()
            .SetMessage("Ocorreu um erro interno. Tente novamente.")
            .SetCode("INTERNAL_ERROR")
            .SetPath(originalError.Path)
            .Build();
    }
} 