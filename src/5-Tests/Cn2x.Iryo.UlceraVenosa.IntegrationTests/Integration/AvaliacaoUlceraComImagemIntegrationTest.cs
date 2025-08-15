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
    public class AvaliacaoUlceraComImagemIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AvaliacaoUlceraComImagemIntegrationTest(DatabaseFixture dbFixture)
        {
            _factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Fact]
        public async Task Deve_Criar_AvaliacaoUlcera_Com_Imagem_Real()
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

            // Criar uma imagem de teste (bytes de uma imagem JPEG válida)
            var imagemBytes = CriarImagemJPEGTeste();
            
            // Agora criar a avaliação com imagem
            var content = CriarUpsertAvaliacaoUlceraComImagemRequest(null, ulceraId, profissionalId, imagemBytes);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL com Imagem: {json}");
            
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertAvaliacaoUlceraAsync");
            
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
        public async Task Deve_Atualizar_AvaliacaoUlcera_Substituindo_Imagem()
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

            // Criar avaliação inicial com primeira imagem
            var imagem1Bytes = CriarImagemJPEGTeste();
            var contentInicial = CriarUpsertAvaliacaoUlceraComImagemRequest(null, ulceraId, profissionalId, imagem1Bytes);
            var responseInicial = await _client.PostAsync("/graphql", contentInicial);
            responseInicial.EnsureSuccessStatusCode();
            var jsonInicial = await responseInicial.Content.ReadAsStringAsync();
            var dictInicial = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonInicial, _jsonOptions);
            var avaliacaoId = Guid.Parse(dictInicial["data"].GetProperty("upsertAvaliacaoUlceraAsync").GetProperty("id").GetString());

            // Agora atualizar a avaliação com uma nova imagem (deve substituir a anterior)
            var imagem2Bytes = CriarImagemJPEGTeste(); // Nova imagem
            var content = CriarUpsertAvaliacaoUlceraComImagemRequest(avaliacaoId, ulceraId, profissionalId, imagem2Bytes);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL Atualização com Nova Imagem: {json}");
            
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertAvaliacaoUlceraAsync");
            
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
        public async Task Deve_Manter_Imagem_Existente_Ao_Atualizar_Sem_Arquivo()
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

            // Criar avaliação inicial com imagem
            var imagemBytes = CriarImagemJPEGTeste();
            var contentInicial = CriarUpsertAvaliacaoUlceraComImagemRequest(null, ulceraId, profissionalId, imagemBytes);
            var responseInicial = await _client.PostAsync("/graphql", contentInicial);
            responseInicial.EnsureSuccessStatusCode();
            var jsonInicial = await responseInicial.Content.ReadAsStringAsync();
            var dictInicial = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonInicial, _jsonOptions);
            var avaliacaoId = Guid.Parse(dictInicial["data"].GetProperty("upsertAvaliacaoUlceraAsync").GetProperty("id").GetString());

            // Agora atualizar a avaliação sem arquivo (deve manter a imagem existente)
            var content = CriarUpsertAvaliacaoUlceraSemImagemRequest(avaliacaoId, ulceraId, profissionalId);

            // Act
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta GraphQL Mantendo Imagem Existente: {json}");
            
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);
            var data = dict["data"].GetProperty("upsertAvaliacaoUlceraAsync");
            
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

        private byte[] CriarImagemJPEGTeste()
        {
            // Criar uma imagem JPEG válida de teste (1x1 pixel)
            // Bytes mágicos JPEG: FF D8 + dados mínimos + FF D9
            var jpegBytes = new byte[]
            {
                0xFF, 0xD8, // SOI marker
                0xFF, 0xE0, // APP0 marker
                0x00, 0x10, // Length
                0x4A, 0x46, 0x49, 0x46, 0x00, // "JFIF\0"
                0x01, 0x01, // Version 1.1
                0x00, // Units: none
                0x00, 0x01, // Density: 1x1
                0x00, 0x01,
                0x00, 0x00, // No thumbnail
                0xFF, 0xDB, // DQT marker
                0x00, 0x43, // Length
                0x00, // Table info
                // Quantization table (simplificada)
                0x10, 0x0B, 0x09, 0x0B, 0x15, 0x18, 0x18, 0x15,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18,
                0xFF, 0xC0, // SOF0 marker
                0x00, 0x11, // Length
                0x08, // Precision
                0x00, 0x01, // Height: 1
                0x00, 0x01, // Width: 1
                0x03, // Components: 3
                0x01, 0x11, 0x00, // Component 1: Y
                0x02, 0x11, 0x01, // Component 2: Cb
                0x03, 0x11, 0x01, // Component 3: Cr
                0xFF, 0xC4, // DHT marker
                0x00, 0x14, // Length
                0x00, // Table info
                // Huffman table (simplificada)
                0x00, 0x01, 0x05, 0x01, 0x01, 0x01, 0x01, 0x01,
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0xFF, 0xDA, // SOS marker
                0x00, 0x0C, // Length
                0x03, // Components: 3
                0x01, 0x00, // Component 1: Y
                0x02, 0x11, // Component 2: Cb
                0x03, 0x11, // Component 3: Cr
                0x00, 0x3F, 0x00, // Spectral selection
                0x00, 0x00, 0x01, 0x05, 0x01, 0x01, 0x01, 0x01, // Dummy data
                0xFF, 0xD9  // EOI marker
            };
            
            return jpegBytes;
        }

        private StringContent CriarUpsertAvaliacaoUlceraComImagemRequest(Guid? id, Guid ulceraId, Guid profissionalId, byte[] imagemBytes)
        {
            var request = new
            {
                query = @"
                    mutation ($input: UpsertAvaliacaoUlceraInput!) {
                        upsertAvaliacaoUlceraAsync(input: $input) {
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
                        arquivo = Convert.ToBase64String(imagemBytes),
                        descricaoImagem = "Imagem de teste da úlcera",
                        dataCapturaImagem = DateTime.UtcNow,
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
                            dor = 2,
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

        private StringContent CriarUpsertAvaliacaoUlceraSemImagemRequest(Guid? id, Guid ulceraId, Guid profissionalId)
        {
            var request = new
            {
                query = @"
                    mutation ($input: UpsertAvaliacaoUlceraInput!) {
                        upsertAvaliacaoUlceraAsync(input: $input) {
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
                        profissionalId = profissionalId,
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
