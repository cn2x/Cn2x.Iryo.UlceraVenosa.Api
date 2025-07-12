using FluentAssertions;
using System.Text;
using System.Text.Json;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{
    public class UlceraGraphQLIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public UlceraGraphQLIntegrationTest(DatabaseFixture dbFixture)
        {
            var factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
            _client = factory.CreateClient();
        }

        private static StringContent CriarUpsertUlceraRequest(Guid? id, Guid pacienteId, Guid topografiaId, int tipoTopografia, object? classificacaoCeap = null)
        {
            var request = new
            {
                query = @"
                mutation ($input: UpsertUlceraInput!) {
                    upsertUlcera(input: $input) {
                        id
                        pacienteId
                        topografiaId
                        tipoTopografia
                        classificacaoCeap {
                          classeClinica
                          etiologia
                          anatomia
                          patofisiologia
                        }
                    }
                }",
                variables = new
                {
                    input = new
                    {
                        id,
                        pacienteId,
                        topografiaId,
                        tipoTopografia,
                        classificacaoCeap
                    }
                }
            };
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task Deve_Upsertar_Ulcera()
        {
            // Arrange: use valores válidos do seed
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var topografiaId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var tipoTopografia = 1; // Exemplo: Perna
            var content = CriarUpsertUlceraRequest(null, pacienteId, topografiaId, tipoTopografia);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlcera");

            // Assert
            data.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            data.GetProperty("topografiaId").GetGuid().Should().Be(topografiaId);
            data.GetProperty("tipoTopografia").GetInt32().Should().Be(tipoTopografia);
        }
    }
}
