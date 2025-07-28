using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;

/// <summary>
/// Extensões para configuração do Entity Framework
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Adiciona o DbContext configurado para PostgreSQL
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
                
                npgsqlOptions.CommandTimeout(30);
            });
            
            // Configurações de performance
            options.EnableSensitiveDataLogging(false);
            options.EnableDetailedErrors(false);
        });

        return services;
    }

    /// <summary>
    /// Configura o DbContext para desenvolvimento
    /// </summary>
    public static IServiceCollection AddApplicationDbContextForDevelopment(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(120),
                    errorCodesToAdd: null);
                
                npgsqlOptions.CommandTimeout(120);
            });
            
            // Configurações para desenvolvimento
            options.EnableSensitiveDataLogging(true);
            options.EnableDetailedErrors(true);
        });

        return services;
    }

    /// <summary>
    /// Aplica snake_case em nomes de tabelas e colunas
    /// </summary>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Tabela
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));
            // Colunas
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.GetColumnName()));
            }
            // Chaves
            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()));
            }
            // Índices
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var stringBuilder = new System.Text.StringBuilder();
        var previousCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(input[0]);
        stringBuilder.Append(char.ToLowerInvariant(input[0]));
        for (int i = 1; i < input.Length; i++)
        {
            var currentChar = input[i];
            var currentCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(currentChar);
            if (currentCategory == System.Globalization.UnicodeCategory.UppercaseLetter &&
                previousCategory != System.Globalization.UnicodeCategory.SpaceSeparator &&
                previousCategory != System.Globalization.UnicodeCategory.UppercaseLetter)
            {
                stringBuilder.Append('_');
                stringBuilder.Append(char.ToLowerInvariant(currentChar));
            }
            else
            {
                stringBuilder.Append(char.ToLowerInvariant(currentChar));
            }
            previousCategory = currentCategory;
        }
        return stringBuilder.ToString();
    }

    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        return services;
    }
} 