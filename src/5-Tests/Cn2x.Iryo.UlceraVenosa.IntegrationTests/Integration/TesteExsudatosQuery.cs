using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class TesteExsudatosQuery : IClassFixture<TestContainerFixture>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public TesteExsudatosQuery(TestContainerFixture dbFixture)
    {
        _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task DeveRetornarTodosOsExsudatos()
    {
        // Arrange
        var query = @"
            query {
                exsudatos {
                    id
                    nome
                    descricao
                    desativada
                }
            }";

        var request = new
        {
            query = query
        };

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonDocument.Parse(responseContent);
        
        // Verifica se não há erros
        jsonResponse.RootElement.TryGetProperty("errors", out var errors).Should().BeFalse();
        
        // Verifica se há dados
        jsonResponse.RootElement.TryGetProperty("data", out var data).Should().BeTrue();
        data.TryGetProperty("exsudatos", out var exsudatos).Should().BeTrue();
        
        // Verifica se retornou uma lista de exsudatos
        var exsudatosArray = exsudatos.EnumerateArray();
        exsudatosArray.Count().Should().BeGreaterThan(0);
        
        // Verifica se cada exsudato tem os campos esperados
        foreach (var exsudato in exsudatosArray)
        {
            exsudato.TryGetProperty("id", out var id).Should().BeTrue();
            exsudato.TryGetProperty("nome", out var nome).Should().BeTrue();
            exsudato.TryGetProperty("descricao", out var descricao).Should().BeTrue();
            exsudato.TryGetProperty("desativada", out var desativada).Should().BeTrue();
            
            // Verifica se os campos não são nulos
            id.ValueKind.Should().NotBe(JsonValueKind.Null);
            nome.ValueKind.Should().NotBe(JsonValueKind.Null);
            descricao.ValueKind.Should().NotBe(JsonValueKind.Null);
            desativada.ValueKind.Should().NotBe(JsonValueKind.Null);
        }
        
        Console.WriteLine($"Query executada com sucesso. Retornou {exsudatosArray.Count()} exsudatos.");
    }
}
