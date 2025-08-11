using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils;
using FluentAssertions;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration
{
    public class AvaliacaoUlceraGraphQLIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AvaliacaoUlceraGraphQLIntegrationTest(DatabaseFixture dbFixture)
        {
            _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Fact]
        public async Task Deve_Upsertar_AvaliacaoUlcera_Com_Profissional()
        {
            // Arrange: use valores válidos do seed
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Utils.TestSeedData.Seed(seedDb);
            }

            var pacienteId = Utils.TestSeedData.PacienteId;
            var profissionalId = Utils.TestSeedData.ProfissionalId;
            var lateralidadeId = Utils.TestSeedData.LateralidadeId;
            var segmentacaoId = Utils.TestSeedData.SegmentacaoId;
            var regiaoAnatomicaId = Utils.TestSeedData.RegiaoAnatomicaId;

            // Primeiro criar uma úlcera
            var ulceraContent = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);
            var ulceraResponse = await _client.PostAsync("/graphql", ulceraContent);
            ulceraResponse.EnsureSuccessStatusCode();
            var ulceraJson = await ulceraResponse.Content.ReadAsStringAsync();
            var ulceraDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ulceraJson, _jsonOptions);
            var ulceraId = Guid.Parse(ulceraDict["data"].GetProperty("upsertUlceraPerna").GetProperty("id").GetString());

            // Agora criar a avaliação
            var content = CriarUpsertAvaliacaoUlceraRequest(null, ulceraId, profissionalId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL: {json}");
            
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertAvaliacaoUlcera");
            
            // Assert
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            var avaliacaoId = Guid.Parse(idStr);
            
            var ulceraIdStr = data.GetProperty("ulceraId").GetString();
            if (string.IsNullOrEmpty(ulceraIdStr)) throw new Exception($"UlceraId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(ulceraIdStr).Should().Be(ulceraId);
            
            var profissionalIdStr = data.GetProperty("profissionalId").GetString();
            if (string.IsNullOrEmpty(profissionalIdStr)) throw new Exception($"ProfissionalId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(profissionalIdStr).Should().Be(profissionalId);
        }

        [Fact]
        public async Task Deve_Atualizar_AvaliacaoUlcera_Com_Profissional()
        {
            // Arrange: use valores válidos do seed
            using (var seedScope = _factory.Services.CreateScope())
            {
                var seedDb = seedScope.ServiceProvider.GetRequiredService<Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.ApplicationDbContext>();
                Utils.TestSeedData.Seed(seedDb);
            }

            var pacienteId = Utils.TestSeedData.PacienteId;
            var profissionalId = Utils.TestSeedData.ProfissionalId;
            var lateralidadeId = Utils.TestSeedData.LateralidadeId;
            var segmentacaoId = Utils.TestSeedData.SegmentacaoId;
            var regiaoAnatomicaId = Utils.TestSeedData.RegiaoAnatomicaId;

            // Primeiro criar uma úlcera
            var ulceraContent = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);
            var ulceraResponse = await _client.PostAsync("/graphql", ulceraContent);
            ulceraResponse.EnsureSuccessStatusCode();
            var ulceraJson = await ulceraResponse.Content.ReadAsStringAsync();
            var ulceraDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ulceraJson, _jsonOptions);
            var ulceraId = Guid.Parse(ulceraDict["data"].GetProperty("upsertUlceraPerna").GetProperty("id").GetString());

            // Criar avaliação inicial
            var contentInicial = CriarUpsertAvaliacaoUlceraRequest(null, ulceraId, profissionalId);
            var responseInicial = await _client.PostAsync("/graphql", contentInicial);
            responseInicial.EnsureSuccessStatusCode();
            var jsonInicial = await responseInicial.Content.ReadAsStringAsync();
            var dictInicial = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonInicial, _jsonOptions);
            var avaliacaoId = Guid.Parse(dictInicial["data"].GetProperty("upsertAvaliacaoUlcera").GetProperty("id").GetString());

            // Agora atualizar a avaliação
            var content = CriarUpsertAvaliacaoUlceraRequest(avaliacaoId, ulceraId, profissionalId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL Atualização: {json}");
            
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertAvaliacaoUlcera");
            
            // Assert
            var idStr = data.GetProperty("id").GetString();
            if (string.IsNullOrEmpty(idStr)) throw new Exception($"ID retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(idStr).Should().Be(avaliacaoId);
            
            var ulceraIdStr = data.GetProperty("ulceraId").GetString();
            if (string.IsNullOrEmpty(ulceraIdStr)) throw new Exception($"UlceraId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(ulceraIdStr).Should().Be(ulceraId);
            
            var profissionalIdStr = data.GetProperty("profissionalId").GetString();
            if (string.IsNullOrEmpty(profissionalIdStr)) throw new Exception($"ProfissionalId retornado nulo ou vazio. Resposta: {json}");
            Guid.Parse(profissionalIdStr).Should().Be(profissionalId);
        }

        [Fact]
        public async Task Deve_Falhar_Ao_Criar_AvaliacaoUlcera_Sem_Profissional()
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

            // Primeiro criar uma úlcera
            var ulceraContent = CriarUpsertUlceraPernaRequest(null, pacienteId, lateralidadeId, segmentacaoId, regiaoAnatomicaId);
            var ulceraResponse = await _client.PostAsync("/graphql", ulceraContent);
            ulceraResponse.EnsureSuccessStatusCode();
            var ulceraJson = await ulceraResponse.Content.ReadAsStringAsync();
            var ulceraDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ulceraJson, _jsonOptions);
            var ulceraId = Guid.Parse(ulceraDict["data"].GetProperty("upsertUlceraPerna").GetProperty("id").GetString());

            // Tentar criar avaliação sem profissional (deve falhar)
            var content = CriarUpsertAvaliacaoUlceraRequestSemProfissional(null, ulceraId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            
            // Assert - deve falhar
            response.IsSuccessStatusCode.Should().BeFalse();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL (esperada falha): {json}");
            
            // Verificar se há erro relacionado ao profissional
            json.Should().Contain("profissional");
        }

        private StringContent CriarUpsertAvaliacaoUlceraRequest(Guid? id, Guid ulceraId, Guid profissionalId)
        {
            var request = new
            {
                query = @"
                    mutation ($input: UpsertAvaliacaoUlceraInput!) {
                        upsertAvaliacaoUlcera(input: $input) {
                            id
                            ulceraId
                            profissionalId
                            dataAvaliacao
                            mesesDuracao
                            caracteristicas {
                                bordasDefinidas
                                tecidoGranulacao
                                necrose
                                odorFetido
                            }
                            sinaisInflamatorios {
                                calor
                                rubor
                                edema
                                dor
                                perdaDeFuncao
                                eritema
                            }
                            medida {
                                comprimento
                                largura
                                profundidade
                            }
                        }
                    }",
                variables = new
                {
                    input = new
                    {
                        id = id,
                        ulceraId = ulceraId,
                        profissionalId = profissionalId,
                        dataAvaliacao = DateTime.UtcNow,
                        mesesDuracao = 2,
                        caracteristicas = new
                        {
                            bordasDefinidas = true,
                            tecidoGranulacao = false,
                            necrose = true,
                            odorFetido = false
                        },
                        sinaisInflamatorios = new
                        {
                            calor = true,
                            rubor = false,
                            edema = true,
                            dor = 2, // Agora é integer
                            perdaDeFuncao = false,
                            eritema = true
                        },
                        medida = new
                        {
                            comprimento = 5.5m,
                            largura = 3.2m,
                            profundidade = 1.0m
                        }
                    }
                }
            };
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }

        private StringContent CriarUpsertAvaliacaoUlceraRequestSemProfissional(Guid? id, Guid ulceraId)
        {
            var request = new
            {
                query = @"
                    mutation ($input: UpsertAvaliacaoUlceraInput!) {
                        upsertAvaliacaoUlcera(input: $input) {
                            id
                            ulceraId
                            profissionalId
                            dataAvaliacao
                            mesesDuracao
                        }
                    }",
                variables = new
                {
                    input = new
                    {
                        id = id,
                        ulceraId = ulceraId,
                        // ProfissionalId omitido intencionalmente
                        dataAvaliacao = DateTime.UtcNow,
                        mesesDuracao = 2
                    }
                }
            };
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }

        private StringContent CriarUpsertUlceraPernaRequest(Guid? id, Guid pacienteId, Guid lateralidadeId, Guid segmentacaoId, Guid regiaoAnatomicaId)
        {
            var request = new
            {
                query = @"
                    mutation ($input: UpsertUlceraPernaInput!) {
                        upsertUlceraPerna(input: $input) {
                            id
                            pacienteId
                            topografia {
                                lateralidadeId
                                segmentacaoId
                                regiaoAnatomicaId
                            }
                            ceap {
                                classeClinica {
                                    id
                                }
                                etiologia {
                                    id
                                }
                                anatomia {
                                    id
                                }
                                patofisiologia {
                                    id
                                }
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
    }
}
