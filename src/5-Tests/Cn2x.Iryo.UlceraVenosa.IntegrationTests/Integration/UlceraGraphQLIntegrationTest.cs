using FluentAssertions;
using System.Text;
using System.Text.Json;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration {
    public class UlceraGraphQLIntegrationTest : IClassFixture<TestContainerFixture>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public UlceraGraphQLIntegrationTest(TestContainerFixture dbFixture)
        {
            _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
            _client = _factory.CreateClient();
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
                        topografia {
                          id
                          ... on TopografiaPerna {
                            lateralidadeId
                            segmentacaoId
                            regiaoAnatomicaId
                          }
                          ... on TopografiaPe {
                            lateralidadeId
                            regiaoTopograficaPeId
                          }
                        }
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

        private static StringContent CriarUpsertUlceraPeRequest(Guid? id, Guid pacienteId, Guid lateralidadeId, Guid regiaoTopograficaPeId, Guid regiaoAnatomicaId)
        {
            var request = new
            {
                query = @"
                mutation ($input: UpsertUlceraPeInput!) {
                    upsertUlceraPe(input: $input) {
                        id
                        pacienteId
                        topografia {
                          id
                          ... on TopografiaPerna {
                            lateralidadeId
                            segmentacaoId
                            regiaoAnatomicaId
                          }
                          ... on TopografiaPe {
                            lateralidadeId
                            regiaoTopograficaPeId
                          }
                        }
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
                            regiaoTopograficaPeId,
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
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Utils.TestSeedData.Seed(seedDb);
            }
            var pacienteId = Utils.TestSeedData.PacienteId;
            var lateralidadeId = Utils.TestSeedData.LateralidadeId;
            var segmentacaoId = Utils.TestSeedData.SegmentacaoId;
            var regiaoAnatomicaId = Utils.TestSeedData.RegiaoAnatomicaId;
            var content = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL: {json}");
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPerna");
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            var ulceraId = Guid.Parse(idStr);
            var pacienteIdStr = data.GetProperty("pacienteId").GetString();
            if (string.IsNullOrEmpty(pacienteIdStr)) throw new Exception($"PacienteId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(pacienteIdStr).Should().Be(pacienteId);

            // Assert resposta da mutation
            var topografia = data.GetProperty("topografia");
            var lateralidadeIdStr = topografia.GetProperty("lateralidadeId").GetString();
            if (string.IsNullOrEmpty(lateralidadeIdStr)) throw new Exception($"LateralidadeId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(lateralidadeIdStr).Should().Be(lateralidadeId);
            if (topografia.TryGetProperty("segmentacaoId", out var segmentacaoIdProp)) {
                var segmentacaoIdStr = segmentacaoIdProp.GetString();
                if (string.IsNullOrEmpty(segmentacaoIdStr)) throw new Exception($"SegmentacaoId retornado nulo ou vazio. Resposta: {json}");
                Guid.Parse(segmentacaoIdStr).Should().Be(segmentacaoId);
            }
            if (topografia.TryGetProperty("regiaoAnatomicaId", out var regiaoAnatomicaIdProp)) {
                var regiaoAnatomicaIdStr = regiaoAnatomicaIdProp.GetString();
                if (string.IsNullOrEmpty(regiaoAnatomicaIdStr)) throw new Exception($"RegiaoAnatomicaId retornado nulo ou vazio. Resposta: {json}");
                Guid.Parse(regiaoAnatomicaIdStr).Should().Be(regiaoAnatomicaId);
            }

            // Validação direta no banco
            using var dbScope = _factory.Services.CreateScope();
            var validaDb = dbScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
            var ulceraDb = await validaDb.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.Id == ulceraId);
            Assert.NotNull(ulceraDb);
            Assert.Equal(pacienteId, ulceraDb.PacienteId);
            Assert.Equal(lateralidadeId, ((TopografiaPerna)ulceraDb.Topografia).LateralidadeId);
            Assert.Equal(segmentacaoId, ((TopografiaPerna)ulceraDb.Topografia).SegmentacaoId);
            Assert.Equal(regiaoAnatomicaId, ((TopografiaPerna)ulceraDb.Topografia).RegiaoAnatomicaId);
            Assert.NotNull(ulceraDb.Ceap);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais, ulceraDb.Ceap.ClasseClinica);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria, ulceraDb.Ceap.Etiologia);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo, ulceraDb.Ceap.Anatomia);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo, ulceraDb.Ceap.Patofisiologia);
        }

        [Fact]
        public async Task Deve_Upsertar_UlceraPe()
        {
            // Arrange: use valores válidos do seed
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(seedDb);
            }
            var pacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId;
            var lateralidadeId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.LateralidadeId;
            var regiaoTopograficaPeId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoTopograficaPeId;
            var regiaoAnatomicaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId;
            var content = CriarUpsertUlceraPeRequest(null, pacienteId, lateralidadeId, regiaoTopograficaPeId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL: {json}");
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPe");
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            var ulceraId = Guid.Parse(idStr);
            var pacienteIdStr = data.GetProperty("pacienteId").GetString();
            if (string.IsNullOrEmpty(pacienteIdStr)) throw new Exception($"PacienteId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(pacienteIdStr).Should().Be(pacienteId);

            // Assert resposta da mutation
            var topografia = data.GetProperty("topografia");
            var lateralidadeIdStr = topografia.GetProperty("lateralidadeId").GetString();
            if (string.IsNullOrEmpty(lateralidadeIdStr)) throw new Exception($"LateralidadeId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(lateralidadeIdStr).Should().Be(lateralidadeId);
            if (topografia.TryGetProperty("regiaoTopograficaPeId", out var regiaoTopograficaPeIdProp)) {
                var regiaoTopograficaPeIdStr = regiaoTopograficaPeIdProp.GetString();
                if (string.IsNullOrEmpty(regiaoTopograficaPeIdStr)) throw new Exception($"RegiaoTopograficaPeId retornado nulo ou vazio. Resposta: {json}");
                Guid.Parse(regiaoTopograficaPeIdStr).Should().Be(regiaoTopograficaPeId);
            }
            if (topografia.TryGetProperty("regiaoAnatomicaId", out var regiaoAnatomicaIdProp)) {
                var regiaoAnatomicaIdStr = regiaoAnatomicaIdProp.GetString();
                if (string.IsNullOrEmpty(regiaoAnatomicaIdStr)) throw new Exception($"RegiaoAnatomicaId retornado nulo ou vazio. Resposta: {json}");
                Guid.Parse(regiaoAnatomicaIdStr).Should().Be(regiaoAnatomicaId);
            }

            // Validação direta no banco
            using var dbScope = _factory.Services.CreateScope();
            var validaDb = dbScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
            var ulceraDb = await validaDb.Ulceras.Include(u => u.Topografia).FirstOrDefaultAsync(u => u.Id == ulceraId);
            Assert.NotNull(ulceraDb);
            Assert.Equal(pacienteId, ulceraDb.PacienteId);
            Assert.Equal(lateralidadeId, ((TopografiaPe)ulceraDb.Topografia).LateralidadeId);
            Assert.Equal(regiaoTopograficaPeId, ((TopografiaPe)ulceraDb.Topografia).RegiaoTopograficaPeId);
            Assert.NotNull(ulceraDb.Ceap);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.Telangiectasias, ulceraDb.Ceap.ClasseClinica);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Congenita, ulceraDb.Ceap.Etiologia);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Superficial, ulceraDb.Ceap.Anatomia);
            Assert.Equal(Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.NaoIdentificada, ulceraDb.Ceap.Patofisiologia);
        }

        [Fact]
        public async Task Deve_Upsertar_E_Recuperar_UlceraPerna()
        {
            // Arrange
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(seedDb);
            }
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var segmentacaoId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            Console.WriteLine($"[DEBUG] IDs usados na mutation perna: pacienteId={pacienteId}, lateralidadeId={lateralidadeId}, segmentacaoId={segmentacaoId}, regiaoAnatomicaId={regiaoAnatomicaId}");

            var ceap = new {
                classeClinica = "SEM_SINAIS",
                etiologia = "PRIMARIA",
                anatomia = "PROFUNDO",
                patofisiologia = "REFLUXO"
            };

            var content = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Mutation (perna): {json}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na mutation: {response.StatusCode}\n{json}");
            }
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPerna");
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            var ulceraId = Guid.Parse(idStr);
            var pacienteIdStr = data.GetProperty("pacienteId").GetString();
            if (string.IsNullOrEmpty(pacienteIdStr)) throw new Exception($"PacienteId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(pacienteIdStr).Should().Be(pacienteId);

            // Assert mutation
            var ceapResp = data.GetProperty("ceap");
            ceapResp.GetProperty("classeClinica").GetProperty("id").GetString().Should().Be("SEM_SINAIS");
            ceapResp.GetProperty("etiologia").GetProperty("id").GetString().Should().Be("PRIMARIA");
            ceapResp.GetProperty("anatomia").GetProperty("id").GetString().Should().Be("PROFUNDO");
            ceapResp.GetProperty("patofisiologia").GetProperty("id").GetString().Should().Be("REFLUXO");

            // Query para recuperar
            var query = new
            {
                query = @"query($id: UUID!) { ulcera(id: $id) { id pacienteId topografia { id ... on TopografiaPerna { lateralidadeId segmentacaoId regiaoAnatomicaId } ... on TopografiaPe { lateralidadeId regiaoTopograficaPeId } } ceap { classeClinica { id name } etiologia { id name } anatomia { id name } patofisiologia { id name } } } }",
                variables = new { id = ulceraId }
            };
            var queryContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var queryResponse = await _client.PostAsync("/graphql", queryContent);
            var queryJson = await queryResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Query (perna): {queryJson}");
            var queryDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(queryJson, _jsonOptions);
            if (!queryDict.ContainsKey("data")) throw new Exception($"Resposta sem campo data: {queryJson}");
            var ulcera = queryDict["data"].GetProperty("ulcera");
            if (!ulcera.TryGetProperty("id", out var ulceraIdElem)) throw new Exception($"Campo id não encontrado: {queryJson}");
            var ulceraIdStr = ulceraIdElem.GetString();
            ulceraIdStr.Should().Be(ulceraId.ToString("N"));
            if (!ulcera.TryGetProperty("pacienteId", out var pacienteIdElem)) throw new Exception($"Campo pacienteId não encontrado: {queryJson}");
            var pacienteIdStr2 = pacienteIdElem.GetString();
            Guid.Parse(pacienteIdStr2).Should().Be(pacienteId);
            if (!ulcera.TryGetProperty("topografia", out var topografiaElem) || topografiaElem.ValueKind == JsonValueKind.Null)
            {
                throw new Exception($"Campo topografia nulo ou ausente na resposta: {queryJson}");
            }
            var lateralidadeIdStr2 = topografiaElem.GetProperty("lateralidadeId").GetString();
            Guid.Parse(lateralidadeIdStr2).Should().Be(lateralidadeId);
            if (topografiaElem.TryGetProperty("segmentacaoId", out var segmentacaoIdElem2))
            {
                var segmentacaoIdStr2 = segmentacaoIdElem2.GetString();
                Guid.Parse(segmentacaoIdStr2).Should().Be(segmentacaoId);
            }
            if (topografiaElem.TryGetProperty("regiaoAnatomicaId", out var regiaoAnatomicaIdElem2))
            {
                var regiaoAnatomicaIdStr2 = regiaoAnatomicaIdElem2.GetString();
                Guid.Parse(regiaoAnatomicaIdStr2).Should().Be(regiaoAnatomicaId);
            }
            var ceapQuery = ulcera.GetProperty("ceap");
            ceapQuery.GetProperty("classeClinica").GetProperty("id").GetString().Should().NotBeNullOrEmpty();
            ceapQuery.GetProperty("etiologia").GetProperty("id").GetString().Should().NotBeNullOrEmpty();
            ceapQuery.GetProperty("anatomia").GetProperty("id").GetString().Should().NotBeNullOrEmpty();
            ceapQuery.GetProperty("patofisiologia").GetProperty("id").GetString().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Deve_Upsertar_E_Recuperar_UlceraPe()
        {
            // Arrange
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var regiaoTopograficaPeId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoTopograficaPeId;
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var ceap = new {
                classeClinica = "Telangiectasias",
                etiologia = "Congenita",
                anatomia = "Superficial",
                patofisiologia = "NaoIdentificada"
            };
            var content = CriarUpsertUlceraPeRequest(null, pacienteId, lateralidadeId, regiaoTopograficaPeId, regiaoAnatomicaId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL: {json}");
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertUlceraPe");
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            var ulceraId = Guid.Parse(idStr);
            var pacienteIdStr = data.GetProperty("pacienteId").GetString();
            if (string.IsNullOrEmpty(pacienteIdStr)) throw new Exception($"PacienteId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(pacienteIdStr).Should().Be(pacienteId);

            // Assert mutation
            var ceapResp = data.GetProperty("ceap");
            ceapResp.GetProperty("classeClinica").GetProperty("id").GetString().Should().Be("TELANGIECTASIAS");
            ceapResp.GetProperty("etiologia").GetProperty("id").GetString().Should().Be("CONGENITA");
            ceapResp.GetProperty("anatomia").GetProperty("id").GetString().Should().Be("SUPERFICIAL");
            ceapResp.GetProperty("patofisiologia").GetProperty("id").GetString().Should().Be("NAO_IDENTIFICADA");

            // Query para recuperar
            var query = new
            {
                query = @"query($id: UUID!) { ulcera(id: $id) { id pacienteId topografia { id ... on TopografiaPerna { lateralidadeId segmentacaoId regiaoAnatomicaId } ... on TopografiaPe { lateralidadeId regiaoTopograficaPeId } } ceap { classeClinica { id name } etiologia { id name } anatomia { id name } patofisiologia { id name } } } }",
                variables = new { id = ulceraId }
            };
            var queryContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var queryResponse = await _client.PostAsync("/graphql", queryContent);
            var queryJson2 = await queryResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL Query: {queryJson2}");
            var queryDict2 = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(queryJson2, _jsonOptions);
            if (!queryDict2.ContainsKey("data")) throw new Exception($"Resposta sem campo data: {queryJson2}");
            var ulcera2 = queryDict2["data"].GetProperty("ulcera");
            if (!ulcera2.TryGetProperty("id", out var ulceraIdElem2)) throw new Exception($"Campo id não encontrado: {queryJson2}");
            var ulceraIdStr2 = ulceraIdElem2.GetString();
            ulceraIdStr2.Should().Be(ulceraId.ToString("N"));
            if (!ulcera2.TryGetProperty("pacienteId", out var pacienteIdElem2)) throw new Exception($"Campo pacienteId não encontrado: {queryJson2}");
            var pacienteIdStr2 = pacienteIdElem2.GetString();
            Guid.Parse(pacienteIdStr2).Should().Be(pacienteId);
            var ceapQuery2 = ulcera2.GetProperty("ceap");
            ceapQuery2.GetProperty("classeClinica").GetProperty("id").GetString().Should().Be("TELANGIECTASIAS");
            ceapQuery2.GetProperty("etiologia").GetProperty("id").GetString().Should().Be("CONGENITA");
            ceapQuery2.GetProperty("anatomia").GetProperty("id").GetString().Should().Be("SUPERFICIAL");
            ceapQuery2.GetProperty("patofisiologia").GetProperty("id").GetString().Should().Be("NAO_IDENTIFICADA");
        }

        [Fact]
        public async Task Deve_Upsertar_Medida()
        {
            // Arrange: seed de entidades relacionadas
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(seedDb);
                // Cria TopografiaPerna com os IDs do seed
                var topografia = new TopografiaPerna {
                    LateralidadeId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.LateralidadeId,
                    SegmentacaoId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.SegmentacaoId,
                    RegiaoAnatomicaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId
                };
                // Cria Ulcera usando a factory
                var ulcera = Cn2x.Iryo.UlceraVenosa.Domain.Factories.UlceraFactory.Create(
                    Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
                    topografia,
                    new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                        Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                        Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                        Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                        Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
                );
                seedDb.Ulceras.Add(ulcera);
                seedDb.SaveChanges();
                var avaliacao = new AvaliacaoUlcera {
                    Id = Guid.NewGuid(),
                    UlceraId = ulcera.Id,
                    ProfissionalId = Utils.TestSeedData.ProfissionalId,
                    DataAvaliacao = DateTime.UtcNow,
                    MesesDuracao = 1,
                    Caracteristicas = new(),
                    SinaisInflamatorios = new(),
                    Medida = new Medida { Comprimento = 0, Largura = 0, Profundidade = 0 }
                };
                seedDb.AvaliacoesUlcera.Add(avaliacao);
                seedDb.SaveChanges();
            }
            // Recupera ids
            Guid avaliacaoId;
            using (var dbScope = _factory.Services.CreateScope())
            {
                var db = dbScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                avaliacaoId = db.AvaliacoesUlcera.AsNoTracking().OrderByDescending(a => a.DataAvaliacao).First().Id;
                var exists = db.AvaliacoesUlcera.Any(a => a.Id == avaliacaoId);
                Console.WriteLine($"[DEBUG] AvaliacaoUlceraId usado: {avaliacaoId}, existe no banco: {exists}");
                Assert.True(exists, $"AvaliacaoUlceraId {avaliacaoId} não existe no banco!");
            }
            // Monta mutation GraphQL para criar Medida
            var mutation = new {
                query = @"mutation ($input: UpsertMedidaInput!) { upsertMedida(input: $input) { comprimento largura profundidade } }",
                variables = new {
                    input = new {
                        avaliacaoUlceraId = avaliacaoId,
                        comprimento = 10.5m,
                        largura = 5.2m,
                        profundidade = 1.1m
                    }
                }
            };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(mutation), System.Text.Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Mutation Medida: {json}");
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertMedida");
            if (data.ValueKind == JsonValueKind.Null)
                throw new Exception($"Mutation upsertMedida retornou null. Resposta: {json}");
            // Assert resposta da mutation
            data.GetProperty("comprimento").GetDecimal().Should().Be(10.5m);
            data.GetProperty("largura").GetDecimal().Should().Be(5.2m);
            data.GetProperty("profundidade").GetDecimal().Should().Be(1.1m);
            // Validação direta no banco
            using var dbScope2 = _factory.Services.CreateScope();
            var validaDb = dbScope2.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
            var avaliacaoDb = await validaDb.AvaliacoesUlcera.AsNoTracking().FirstOrDefaultAsync(a => a.Id == avaliacaoId);
            Assert.NotNull(avaliacaoDb);
            Assert.NotNull(avaliacaoDb.Medida);
            avaliacaoDb.Medida.Comprimento.Should().Be(10.5m);
            avaliacaoDb.Medida.Largura.Should().Be(5.2m);
            avaliacaoDb.Medida.Profundidade.Should().Be(1.1m);
        }

        [Fact]
        public async Task Deve_Buscar_Todas_Ulceras_Do_Paciente()
        {
            // Arrange
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.Seed(seedDb);
            }
            
            var pacienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var lateralidadeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var segmentacaoId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var regiaoAnatomicaId = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Criar primeira úlcera (Perna)
            var content1 = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);
            var response1 = await _client.PostAsync("/graphql", content1);
            var json1 = await response1.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Mutation 1 (Perna): {json1}");
            
            if (!response1.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na mutation 1: {response1.StatusCode}\n{json1}");
            }

            // Criar segunda úlcera (Pé)
            var regiaoTopograficaPeId = Utils.TestSeedData.RegiaoTopograficaPeId;
            var content2 = CriarUpsertUlceraPeRequest(null, pacienteId, lateralidadeId, regiaoTopograficaPeId, regiaoAnatomicaId);
            var response2 = await _client.PostAsync("/graphql", content2);
            var json2 = await response2.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Mutation 2 (Pé): {json2}");
            
            if (!response2.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na mutation 2: {response2.StatusCode}\n{json2}");
            }

            // Query para buscar todas as úlceras do paciente
            var queryRequest = new
            {
                query = @"
                query ($pacienteId: UUID!) {
                    ulcerasByPaciente(pacienteId: $pacienteId) {
                        id
                        pacienteId
                        topografia {
                          id
                          ... on TopografiaPerna {
                            lateralidadeId
                            segmentacaoId
                            regiaoAnatomicaId
                          }
                          ... on TopografiaPe {
                            lateralidadeId
                            regiaoTopograficaPeId
                          }
                        }
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
                    pacienteId = pacienteId.ToString()
                }
            };

            var queryContent = new StringContent(JsonSerializer.Serialize(queryRequest), Encoding.UTF8, "application/json");

            // Act
            var queryResponse = await _client.PostAsync("/graphql", queryContent);
            var queryJson = await queryResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Resposta GraphQL Query: {queryJson}");

            // Assert
            queryResponse.IsSuccessStatusCode.Should().BeTrue();
            var queryDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(queryJson, _jsonOptions);
            var ulceras = queryDict["data"].GetProperty("ulcerasByPaciente");
            
            // Deve retornar uma lista com pelo menos 2 úlceras
            var count = ulceras.GetArrayLength();
            count.Should().BeGreaterThanOrEqualTo(2);
            
            // Verificar se todas as úlceras pertencem ao paciente correto
            foreach (var ulcera in ulceras.EnumerateArray())
            {
                var ulceraPacienteId = ulcera.GetProperty("pacienteId").GetString();
                Guid.Parse(ulceraPacienteId).Should().Be(pacienteId);
            }
        }
    }
}
