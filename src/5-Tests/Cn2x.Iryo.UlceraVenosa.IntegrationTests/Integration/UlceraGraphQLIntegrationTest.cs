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

        private static StringContent CriarUpsertUlceraPernaRequest(Guid? id, Guid pacienteId, Guid lateralidadeId, Guid segmentacaoId, Guid regiaoAnatomicaId)
        {
            var request = new
            {
                query = @"
                mutation ($input: UpsertUlceraPernaInput!) {
                    upsertUlceraPerna(input: $input) {
                        id
                        pacienteId
                        ceap {
                          classeClinica { id name }
                          etiologia { id name }
                          anatomia { id name }
                          patofisiologia { id name }
                        }
                    }
                }",
                variables = new
                {
                    input = new
                    {
                        id,
                        pacienteId,
                        topografia = new {
                            lateralidadeId,
                            segmentacaoId,
                            regiaoAnatomicaId
                        },
                        classificacaoCeap = new {
                            classeClinica = "SEM_SINAIS",
                            etiologia = "PRIMARIA",
                            anatomia = "PROFUNDO",
                            patofisiologia = "REFLUXO"
                        }
                    }
                }
            };
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }

        private static StringContent CriarUpsertUlceraPeRequest(Guid? id, Guid pacienteId, Guid lateralidadeId, Guid regiaoAnatomicaId)
        {
            var request = new
            {
                query = @"
                mutation ($input: UpsertUlceraPeInput!) {
                    upsertUlceraPe(input: $input) {
                        id
                        pacienteId
                        ceap {
                          classeClinica { id name }
                          etiologia { id name }
                          anatomia { id name }
                          patofisiologia { id name }
                        }
                    }
                }",
                variables = new
                {
                    input = new
                    {
                        id,
                        pacienteId,
                        topografia = new {
                            lateralidadeId,
                            regiaoAnatomicaId
                        },
                        classificacaoCeap = new {
                            classeClinica = "TELANGIECTASIAS",
                            etiologia = "CONGENITA",
                            anatomia = "SUPERFICIAL",
                            patofisiologia = "NAO_IDENTIFICADA"
                        }
                    }
                }
            };
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task Deve_Upsertar_UlceraPerna()
        {
            // Arrange: use valores válidos do seed
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var segmentacaoId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var content = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPerna");

            // Assert
            data.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var topografia = data.GetProperty("topografia");
            topografia.GetProperty("lateralidadeId").GetGuid().Should().Be(lateralidadeId);
            topografia.GetProperty("segmentacaoId").GetGuid().Should().Be(segmentacaoId);
            topografia.GetProperty("regiaoAnatomicaId").GetGuid().Should().Be(regiaoAnatomicaId);
        }

        [Fact]
        public async Task Deve_Upsertar_UlceraPe()
        {
            // Arrange: use valores válidos do seed
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var content = CriarUpsertUlceraPeRequest(null, pacienteId, lateralidadeId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPe");

            // Assert
            data.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var topografia = data.GetProperty("topografia");
            topografia.GetProperty("lateralidadeId").GetGuid().Should().Be(lateralidadeId);
            topografia.GetProperty("regiaoAnatomicaId").GetGuid().Should().Be(regiaoAnatomicaId);
        }

        [Fact]
        public async Task Deve_Upsertar_E_Recuperar_UlceraPerna()
        {
            // Arrange
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var segmentacaoId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var ceap = new {
                classeClinica = "SemSinais",
                etiologia = "Primaria",
                anatomia = "Profundo",
                patofisiologia = "Refluxo"
            };
            var content = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na mutation: {response.StatusCode}\n{json}");
            }
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPerna");
            var ulceraId = data.GetProperty("id").GetGuid();

            // Assert mutation
            data.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var ceapResp = data.GetProperty("ceap");
            ceapResp.GetProperty("classeClinica").GetInt32().Should().Be(1);
            ceapResp.GetProperty("etiologia").GetInt32().Should().Be(2);
            ceapResp.GetProperty("anatomia").GetInt32().Should().Be(3);
            ceapResp.GetProperty("patofisiologia").GetInt32().Should().Be(1);

            // Query para recuperar
            var query = new
            {
                query = @"query($id: UUID!) { ulcera(id: $id) { id pacienteId ceap { classeClinica { id name } etiologia { id name } anatomia { id name } patofisiologia { id name } } } }",
                variables = new { id = ulceraId }
            };
            var queryContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var queryResponse = await _client.PostAsync("/graphql", queryContent);
            var queryJson = await queryResponse.Content.ReadAsStringAsync();
            if (!queryResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na query: {queryResponse.StatusCode}\n{queryJson}");
            }
            var queryDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(queryJson, _jsonOptions);
            var ulcera = queryDict["data"].GetProperty("ulcera");
            ulcera.GetProperty("id").GetGuid().Should().Be(ulceraId);
            ulcera.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var ceapQuery = ulcera.GetProperty("ceap");
            ceapQuery.GetProperty("classeClinica").GetInt32().Should().Be(1);
            ceapQuery.GetProperty("etiologia").GetInt32().Should().Be(2);
            ceapQuery.GetProperty("anatomia").GetInt32().Should().Be(3);
            ceapQuery.GetProperty("patofisiologia").GetInt32().Should().Be(1);
        }

        [Fact]
        public async Task Deve_Upsertar_E_Recuperar_UlceraPe()
        {
            // Arrange
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var ceap = new {
                classeClinica = "Telangiectasias",
                etiologia = "Congenita",
                anatomia = "Superficial",
                patofisiologia = "NaoIdentificada"
            };
            var content = CriarUpsertUlceraPeRequest(null, pacienteId, lateralidadeId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPe");
            var ulceraId = data.GetProperty("id").GetGuid();

            // Assert mutation
            data.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var ceapResp = data.GetProperty("ceap");
            ceapResp.GetProperty("classeClinica").GetInt32().Should().Be(2);
            ceapResp.GetProperty("etiologia").GetInt32().Should().Be(1);
            ceapResp.GetProperty("anatomia").GetInt32().Should().Be(2);
            ceapResp.GetProperty("patofisiologia").GetInt32().Should().Be(3);

            // Query para recuperar
            var query = new
            {
                query = @"query($id: UUID!) { ulcera(id: $id) { id pacienteId ceap { classeClinica { id name } etiologia { id name } anatomia { id name } patofisiologia { id name } } } }",
                variables = new { id = ulceraId }
            };
            var queryContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var queryResponse = await _client.PostAsync("/graphql", queryContent);
            queryResponse.EnsureSuccessStatusCode();
            var queryJson = await queryResponse.Content.ReadAsStringAsync();
            var queryDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(queryJson, _jsonOptions);
            var ulcera = queryDict["data"].GetProperty("ulcera");
            ulcera.GetProperty("id").GetGuid().Should().Be(ulceraId);
            ulcera.GetProperty("pacienteId").GetGuid().Should().Be(pacienteId);
            var ceapQuery = ulcera.GetProperty("ceap");
            ceapQuery.GetProperty("classeClinica").GetInt32().Should().Be(2);
            ceapQuery.GetProperty("etiologia").GetInt32().Should().Be(1);
            ceapQuery.GetProperty("anatomia").GetInt32().Should().Be(2);
            ceapQuery.GetProperty("patofisiologia").GetInt32().Should().Be(3);
        }
    }
}
