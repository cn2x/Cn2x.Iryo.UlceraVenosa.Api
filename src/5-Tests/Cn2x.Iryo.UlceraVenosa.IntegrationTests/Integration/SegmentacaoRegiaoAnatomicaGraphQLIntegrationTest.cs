using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class SegmentacaoRegiaoAnatomicaGraphQLIntegrationTest : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public SegmentacaoRegiaoAnatomicaGraphQLIntegrationTest(DatabaseFixture dbFixture)
    {
        _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Buscar_Todas_As_Segmentacoes()
    {
        // Arrange
        var query = new
        {
            query = @"query { segmentacoes { id sigla descricao } }"
        };

        var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
        
        dict.Should().ContainKey("data");
        var data = dict["data"];
        data.GetProperty("segmentacoes").Should().NotBeNull();
        
        var segmentacoes = data.GetProperty("segmentacoes");
        segmentacoes.GetArrayLength().Should().BeGreaterThan(0);
        
        // Verificar se as segmentações esperadas estão presentes
        var segmentacoesArray = segmentacoes.EnumerateArray().ToList();
        segmentacoesArray.Should().Contain(s => s.GetProperty("sigla").GetString() == "TS");
        segmentacoesArray.Should().Contain(s => s.GetProperty("sigla").GetString() == "TM");
        segmentacoesArray.Should().Contain(s => s.GetProperty("sigla").GetString() == "TI");
    }

    [Fact]
    public async Task Deve_Buscar_Todas_As_RegioesAnatomicasPerna()
    {
        // Arrange
        var query = new
        {
            query = @"query { regioesAnatomicasPerna { id sigla descricao } }"
        };

        var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
        
        dict.Should().ContainKey("data");
        var data = dict["data"];
        data.GetProperty("regioesAnatomicasPerna").Should().NotBeNull();
        
        var regioesAnatomicas = data.GetProperty("regioesAnatomicasPerna");
        regioesAnatomicas.GetArrayLength().Should().BeGreaterThan(0);
        
        // Verificar se as regiões anatômicas esperadas estão presentes
        var regioesArray = regioesAnatomicas.EnumerateArray().ToList();
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "M");
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "L");
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "A");
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "P");
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "AM");
        regioesArray.Should().Contain(r => r.GetProperty("sigla").GetString() == "PL");
    }
} 