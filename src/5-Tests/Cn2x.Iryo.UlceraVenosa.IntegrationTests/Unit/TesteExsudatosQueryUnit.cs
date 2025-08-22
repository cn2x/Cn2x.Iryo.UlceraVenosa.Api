using System.Text.Json;
using FluentAssertions;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Application.Features.Exsudato.GraphQL.Queries;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Unit;

public class TesteExsudatosQueryUnit
{
    [Fact]
    public void DeveTerQueryExsudatosRegistrada()
    {
        // Arrange & Act
        var queryType = typeof(ExsudatoQueries);
        
        // Assert
        queryType.Should().NotBeNull();
        queryType.Name.Should().Be("ExsudatoQueries");
        
        // Verifica se tem o atributo ExtendObjectType
        var attributes = queryType.GetCustomAttributes(false);
        attributes.Should().NotBeEmpty();
        
        // Verifica se tem o método Exsudatos
        var method = queryType.GetMethod("Exsudatos");
        method.Should().NotBeNull();
        method!.Name.Should().Be("Exsudatos");
        
        // Verifica se retorna List<Exsudato>
        method.ReturnType.Should().Be(typeof(Task<List<Exsudato>>));
        
        Console.WriteLine($"✅ Query Exsudatos está corretamente implementada:");
        Console.WriteLine($"   - Classe: {queryType.Name}");
        Console.WriteLine($"   - Método: {method.Name}");
        Console.WriteLine($"   - Retorno: {method.ReturnType}");
    }
    
    [Fact]
    public void DeveTerExsudatoTypeRegistrado()
    {
        // Arrange & Act
        var exsudatoType = typeof(Exsudato);
        
        // Assert
        exsudatoType.Should().NotBeNull();
        exsudatoType.Name.Should().Be("Exsudato");
        
        // Verifica se tem as propriedades esperadas
        var idProperty = exsudatoType.GetProperty("Id");
        var nomeProperty = exsudatoType.GetProperty("Nome");
        var descricaoProperty = exsudatoType.GetProperty("Descricao");
        var desativadaProperty = exsudatoType.GetProperty("Desativada");
        
        idProperty.Should().NotBeNull();
        nomeProperty.Should().NotBeNull();
        descricaoProperty.Should().NotBeNull();
        desativadaProperty.Should().NotBeNull();
        
        Console.WriteLine($"✅ Entidade Exsudato está corretamente implementada:");
        Console.WriteLine($"   - Id: {idProperty?.PropertyType}");
        Console.WriteLine($"   - Nome: {nomeProperty?.PropertyType}");
        Console.WriteLine($"   - Descricao: {descricaoProperty?.PropertyType}");
        Console.WriteLine($"   - Desativada: {desativadaProperty?.PropertyType}");
    }
}
