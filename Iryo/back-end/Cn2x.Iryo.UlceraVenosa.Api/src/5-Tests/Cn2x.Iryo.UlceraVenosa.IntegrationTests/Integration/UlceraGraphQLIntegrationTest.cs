using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.Exceptions;
using Cn2x.Iryo.UlceraVenosa.Domain.Repositories;
using Cn2x.Iryo.UlceraVenosa.Domain.Services;
using Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace Cn2x.Iryo.UlceraVenosa.IntegrationTests.Integration;

public class UlceraGraphQLIntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly Mock<IUlceraRepository> _ulceraRepositoryMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<ITopografiaRepository> _topografiaRepositoryMock;
    private readonly Mock<ICeapRepository> _ceapRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEventBus> _eventBusMock;

    public UlceraGraphQLIntegrationTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _ulceraRepositoryMock = new Mock<IUlceraRepository>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _topografiaRepositoryMock = new Mock<ITopografiaRepository>();
        _ceapRepositoryMock = new Mock<ICeapRepository>();
        _mediatorMock = new Mock<IMediator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _eventBusMock = new Mock<IEventBus>();
    }

    [Fact]
    public async Task CreateUlcera_ShouldReturnUlcera()
    {
        // Arrange
        _ulceraRepositoryMock.Setup(x => x.Add(It.IsAny<Ulcera>())).ReturnsAsync(new Ulcera {
            Id = Guid.NewGuid(),
            PacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
            TopografiaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, // simplificado
            Topografia = null!, // ajuste conforme necessário
            Ceap = new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
        });
        _pacienteRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Paciente { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId, Nome = "Test Paciente" });
        _topografiaRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Topografia { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, Nome = "Test Topografia" });
        _ceapRepositoryMock.Setup(x => x.Add(It.IsAny<Ceap>())).ReturnsAsync(new Ceap { Id = Guid.NewGuid(), UlceraId = It.IsAny<Guid>(), Clinica = Clinica.SemSinais, Etiologica = Etiologica.Primaria, Anatomica = Anatomica.Profundo, Patofisiologica = Patofisiologica.Refluxo });
        _unitOfWorkMock.Setup(x => x.Commit()).ReturnsAsync(1);
        _eventBusMock.Setup(x => x.Publish(It.IsAny<DomainEvent>())).Returns(Task.CompletedTask);

        var query = @"
            mutation {
                createUlcera(input: {
                    pacienteId: """ + Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId + @"""
                    topografiaId: """ + Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId + @"""
                    ceap: {
                        clinica: SemSinais
                        etiologica: Primaria
                        anatomica: Profundo
                        patofisiologica: Refluxo
                    }
                }) {
                    id
                    pacienteId
                    topografiaId
                    ceap {
                        clinica
                        etiologica
                        anatomica
                        patofisiologica
                    }
                }
            }";

        // Act
        var response = await _client.PostGraphQLAsync(query);

        // Assert
        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull();
        response.Data.ShouldBeOfType<JObject>();
        var ulcera = response.Data.ToObject<JObject>();
        ulcera.ShouldNotBeNull();
        ulcera["createUlcera"].ShouldNotBeNull();
        var createdUlcera = ulcera["createUlcera"].ToObject<JObject>();
        createdUlcera.ShouldNotBeNull();
        createdUlcera["id"].ShouldNotBeNull();
        createdUlcera["pacienteId"].ShouldNotBeNull();
        createdUlcera["topografiaId"].ShouldNotBeNull();
        createdUlcera["ceap"].ShouldNotBeNull();
        var ceap = createdUlcera["ceap"].ToObject<JObject>();
        ceap.ShouldNotBeNull();
        ceap["clinica"].ShouldBe(Clinica.SemSinais.ToString());
        ceap["etiologica"].ShouldBe(Etiologica.Primaria.ToString());
        ceap["anatomica"].ShouldBe(Anatomica.Profundo.ToString());
        ceap["patofisiologica"].ShouldBe(Patofisiologica.Refluxo.ToString());
    }

    [Fact]
    public async Task GetUlceraById_ShouldReturnUlcera()
    {
        // Arrange
        var ulcera = new Ulcera {
            Id = Guid.NewGuid(),
            PacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
            TopografiaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, // simplificado
            Topografia = null!, // ajuste conforme necessário
            Ceap = new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
        };
        _ulceraRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(ulcera);
        _pacienteRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Paciente { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId, Nome = "Test Paciente" });
        _topografiaRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Topografia { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, Nome = "Test Topografia" });
        _ceapRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Ceap { Id = ulcera.Ceap.Id, UlceraId = ulcera.Id, Clinica = Clinica.SemSinais, Etiologica = Etiologica.Primaria, Anatomica = Anatomica.Profundo, Patofisiologica = Patofisiologica.Refluxo });

        var query = @"
            query {
                ulcera(id: """ + ulcera.Id + @""") {
                    id
                    pacienteId
                    topografiaId
                    ceap {
                        clinica
                        etiologica
                        anatomica
                        patofisiologica
                    }
                }
            }";

        // Act
        var response = await _client.PostGraphQLAsync(query);

        // Assert
        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull();
        response.Data.ShouldBeOfType<JObject>();
        var ulceraData = response.Data.ToObject<JObject>();
        ulceraData.ShouldNotBeNull();
        ulceraData["ulcera"].ShouldNotBeNull();
        var ulceraResult = ulceraData["ulcera"].ToObject<JObject>();
        ulceraResult.ShouldNotBeNull();
        ulceraResult["id"].ShouldNotBeNull();
        ulceraResult["pacienteId"].ShouldNotBeNull();
        ulceraResult["topografiaId"].ShouldNotBeNull();
        ulceraResult["ceap"].ShouldNotBeNull();
        var ceapResult = ulceraResult["ceap"].ToObject<JObject>();
        ceapResult.ShouldNotBeNull();
        ceapResult["clinica"].ShouldBe(Clinica.SemSinais.ToString());
        ceapResult["etiologica"].ShouldBe(Etiologica.Primaria.ToString());
        ceapResult["anatomica"].ShouldBe(Anatomica.Profundo.ToString());
        ceapResult["patofisiologica"].ShouldBe(Patofisiologica.Refluxo.ToString());
    }

    [Fact]
    public async Task GetUlceraByPacienteId_ShouldReturnUlcera()
    {
        // Arrange
        var ulcera = new Ulcera {
            Id = Guid.NewGuid(),
            PacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
            TopografiaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, // simplificado
            Topografia = null!, // ajuste conforme necessário
            Ceap = new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
        };
        _ulceraRepositoryMock.Setup(x => x.GetByPacienteId(It.IsAny<Guid>())).ReturnsAsync(new List<Ulcera> { ulcera });
        _pacienteRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Paciente { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId, Nome = "Test Paciente" });
        _topografiaRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Topografia { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, Nome = "Test Topografia" });
        _ceapRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Ceap { Id = ulcera.Ceap.Id, UlceraId = ulcera.Id, Clinica = Clinica.SemSinais, Etiologica = Etiologica.Primaria, Anatomica = Anatomica.Profundo, Patofisiologica = Patofisiologica.Refluxo });

        var query = @"
            query {
                ulceras(pacienteId: """ + Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId + @""") {
                    id
                    pacienteId
                    topografiaId
                    ceap {
                        clinica
                        etiologica
                        anatomica
                        patofisiologica
                    }
                }
            }";

        // Act
        var response = await _client.PostGraphQLAsync(query);

        // Assert
        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull();
        response.Data.ShouldBeOfType<JObject>();
        var ulceraData = response.Data.ToObject<JObject>();
        ulceraData.ShouldNotBeNull();
        ulceraData["ulceras"].ShouldNotBeNull();
        var ulceraResults = ulceraData["ulceras"].ToObject<JArray>();
        ulceraResults.ShouldNotBeNull();
        ulceraResults.Count.ShouldBe(1);
        var ulceraResult = ulceraResults[0].ToObject<JObject>();
        ulceraResult.ShouldNotBeNull();
        ulceraResult["id"].ShouldNotBeNull();
        ulceraResult["pacienteId"].ShouldNotBeNull();
        ulceraResult["topografiaId"].ShouldNotBeNull();
        ulceraResult["ceap"].ShouldNotBeNull();
        var ceapResult = ulceraResult["ceap"].ToObject<JObject>();
        ceapResult.ShouldNotBeNull();
        ceapResult["clinica"].ShouldBe(Clinica.SemSinais.ToString());
        ceapResult["etiologica"].ShouldBe(Etiologica.Primaria.ToString());
        ceapResult["anatomica"].ShouldBe(Anatomica.Profundo.ToString());
        ceapResult["patofisiologica"].ShouldBe(Patofisiologica.Refluxo.ToString());
    }

    [Fact]
    public async Task UpdateUlcera_ShouldReturnUlcera()
    {
        // Arrange
        var ulcera = new Ulcera {
            Id = Guid.NewGuid(),
            PacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
            TopografiaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, // simplificado
            Topografia = null!, // ajuste conforme necessário
            Ceap = new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
        };
        _ulceraRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(ulcera);
        _pacienteRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Paciente { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId, Nome = "Test Paciente" });
        _topografiaRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Topografia { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, Nome = "Test Topografia" });
        _ceapRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Ceap { Id = ulcera.Ceap.Id, UlceraId = ulcera.Id, Clinica = Clinica.SemSinais, Etiologica = Etiologica.Primaria, Anatomica = Anatomica.Profundo, Patofisiologica = Patofisiologica.Refluxo });
        _ulceraRepositoryMock.Setup(x => x.Update(It.IsAny<Ulcera>())).ReturnsAsync(ulcera);
        _unitOfWorkMock.Setup(x => x.Commit()).ReturnsAsync(1);
        _eventBusMock.Setup(x => x.Publish(It.IsAny<DomainEvent>())).Returns(Task.CompletedTask);

        var query = @"
            mutation {
                updateUlcera(id: """ + ulcera.Id + @""", input: {
                    pacienteId: """ + Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId + @"""
                    topografiaId: """ + Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId + @"""
                    ceap: {
                        clinica: SemSinais
                        etiologica: Primaria
                        anatomica: Profundo
                        patofisiologica: Refluxo
                    }
                }) {
                    id
                    pacienteId
                    topografiaId
                    ceap {
                        clinica
                        etiologica
                        anatomica
                        patofisiologica
                    }
                }
            }";

        // Act
        var response = await _client.PostGraphQLAsync(query);

        // Assert
        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull();
        response.Data.ShouldBeOfType<JObject>();
        var ulceraData = response.Data.ToObject<JObject>();
        ulceraData.ShouldNotBeNull();
        ulceraData["updateUlcera"].ShouldNotBeNull();
        var updatedUlcera = ulceraData["updateUlcera"].ToObject<JObject>();
        updatedUlcera.ShouldNotBeNull();
        updatedUlcera["id"].ShouldNotBeNull();
        updatedUlcera["pacienteId"].ShouldNotBeNull();
        updatedUlcera["topografiaId"].ShouldNotBeNull();
        updatedUlcera["ceap"].ShouldNotBeNull();
        var ceapResult = updatedUlcera["ceap"].ToObject<JObject>();
        ceapResult.ShouldNotBeNull();
        ceapResult["clinica"].ShouldBe(Clinica.SemSinais.ToString());
        ceapResult["etiologica"].ShouldBe(Etiologica.Primaria.ToString());
        ceapResult["anatomica"].ShouldBe(Anatomica.Profundo.ToString());
        ceapResult["patofisiologica"].ShouldBe(Patofisiologica.Refluxo.ToString());
    }

    [Fact]
    public async Task DeleteUlcera_ShouldReturnUlcera()
    {
        // Arrange
        var ulcera = new Ulcera {
            Id = Guid.NewGuid(),
            PacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId,
            TopografiaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, // simplificado
            Topografia = null!, // ajuste conforme necessário
            Ceap = new Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects.Ceap(
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Clinica.SemSinais,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Etiologica.Primaria,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Anatomica.Profundo,
                Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes.Patofisiologica.Refluxo)
        };
        _ulceraRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(ulcera);
        _pacienteRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Paciente { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId, Nome = "Test Paciente" });
        _topografiaRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Topografia { Id = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId, Nome = "Test Topografia" });
        _ceapRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Ceap { Id = ulcera.Ceap.Id, UlceraId = ulcera.Id, Clinica = Clinica.SemSinais, Etiologica = Etiologica.Primaria, Anatomica = Anatomica.Profundo, Patofisiologica = Patofisiologica.Refluxo });
        _ulceraRepositoryMock.Setup(x => x.Delete(It.IsAny<Ulcera>())).ReturnsAsync(ulcera);
        _unitOfWorkMock.Setup(x => x.Commit()).ReturnsAsync(1);
        _eventBusMock.Setup(x => x.Publish(It.IsAny<DomainEvent>())).Returns(Task.CompletedTask);

        var query = @"
            mutation {
                deleteUlcera(id: """ + ulcera.Id + @""") {
                    id
                    pacienteId
                    topografiaId
                    ceap {
                        clinica
                        etiologica
                        anatomica
                        patofisiologica
                    }
                }
            }";

        // Act
        var response = await _client.PostGraphQLAsync(query);

        // Assert
        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull();
        response.Data.ShouldBeOfType<JObject>();
        var ulceraData = response.Data.ToObject<JObject>();
        ulceraData.ShouldNotBeNull();
        ulceraData["deleteUlcera"].ShouldNotBeNull();
        var deletedUlcera = ulceraData["deleteUlcera"].ToObject<JObject>();
        deletedUlcera.ShouldNotBeNull();
        deletedUlcera["id"].ShouldNotBeNull();
        deletedUlcera["pacienteId"].ShouldNotBeNull();
        deletedUlcera["topografiaId"].ShouldNotBeNull();
        deletedUlcera["ceap"].ShouldNotBeNull();
        var ceapResult = deletedUlcera["ceap"].ToObject<JObject>();
        ceapResult.ShouldNotBeNull();
        ceapResult["clinica"].ShouldBe(Clinica.SemSinais.ToString());
        ceapResult["etiologica"].ShouldBe(Etiologica.Primaria.ToString());
        ceapResult["anatomica"].ShouldBe(Anatomica.Profundo.ToString());
        ceapResult["patofisiologica"].ShouldBe(Patofisiologica.Refluxo.ToString());
    }

    [Fact]
    public async Task Deve_Upsertar_E_Consultar_Medida_Por_AvaliacaoUlcera()
    {
        // Arrange: criar Ulcera e AvaliacaoUlcera via mutation GraphQL
        var pacienteId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.PacienteId;
        var lateralidadeId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.LateralidadeId;
        var segmentacaoId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.SegmentacaoId;
        var regiaoAnatomicaId = Cn2x.Iryo.UlceraVenosa.IntegrationTests.Utils.TestSeedData.RegiaoAnatomicaId;
        // 1. Criar Ulcera
        var mutationUlcera = new {
            query = @"mutation ($input: UpsertUlceraPernaInput!) { upsertUlceraPerna(input: $input) { id topografia { id } } }",
            variables = new {
                input = new {
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
        var contentUlcera = new StringContent(System.Text.Json.JsonSerializer.Serialize(mutationUlcera), System.Text.Encoding.UTF8, "application/json");
        var responseUlcera = await _client.PostAsync("/graphql", contentUlcera);
        responseUlcera.EnsureSuccessStatusCode();
        var jsonUlcera = await responseUlcera.Content.ReadAsStringAsync();
        var dictUlcera = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(jsonUlcera, _jsonOptions);
        var ulceraData = dictUlcera["data"].GetProperty("upsertUlceraPerna");
        var ulceraId = ulceraData.GetProperty("id").GetString();
        var topografiaId = ulceraData.GetProperty("topografia").GetProperty("id").GetString();
        // 2. Criar AvaliacaoUlcera
        var mutationAvaliacao = new {
            query = @"mutation ($input: UpsertAvaliacaoUlceraInput!) { upsertAvaliacaoUlcera(input: $input) { id } }",
            variables = new {
                input = new {
                    ulceraId,
                    dataAvaliacao = System.DateTime.UtcNow,
                    mesesDuracao = 1,
                    caracteristicas = new { },
                    sinaisInflamatorios = new { },
                    // Se AvaliacaoUlcera exigir topografiaId, inclua aqui:
                    // topografiaId = topografiaId
                }
            }
        };
        var contentAvaliacao = new StringContent(System.Text.Json.JsonSerializer.Serialize(mutationAvaliacao), System.Text.Encoding.UTF8, "application/json");
        var responseAvaliacao = await _client.PostAsync("/graphql", contentAvaliacao);
        responseAvaliacao.EnsureSuccessStatusCode();
        var jsonAvaliacao = await responseAvaliacao.Content.ReadAsStringAsync();
        var dictAvaliacao = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(jsonAvaliacao, _jsonOptions);
        var avaliacaoUlceraId = dictAvaliacao["data"].GetProperty("upsertAvaliacaoUlcera").GetProperty("id").GetString();
        // 3. Upsert Medida
        var mutationMedida = new {
            query = @"mutation ($input: UpsertMedidaInput!) { upsertMedida(input: $input) { id avaliacaoUlceraId comprimento largura profundidade } }",
            variables = new {
                input = new {
                    avaliacaoUlceraId,
                    comprimento = 10.5m,
                    largura = 5.2m,
                    profundidade = 1.1m
                }
            }
        };
        var contentMedida = new StringContent(System.Text.Json.JsonSerializer.Serialize(mutationMedida), System.Text.Encoding.UTF8, "application/json");
        var responseMedida = await _client.PostAsync("/graphql", contentMedida);
        responseMedida.EnsureSuccessStatusCode();
        var jsonMedida = await responseMedida.Content.ReadAsStringAsync();
        var dictMedida = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(jsonMedida, _jsonOptions);
        var medidaData = dictMedida["data"].GetProperty("upsertMedida");
        // Assert mutation
        medidaData.GetProperty("avaliacaoUlceraId").GetString().ShouldBe(avaliacaoUlceraId);
        medidaData.GetProperty("comprimento").GetDecimal().ShouldBe(10.5m);
        medidaData.GetProperty("largura").GetDecimal().ShouldBe(5.2m);
        medidaData.GetProperty("profundidade").GetDecimal().ShouldBe(1.1m);
        // 4. Query Medida
        var queryMedida = new {
            query = @"query ($avaliacaoUlceraId: UUID!) { medidaByAvaliacaoUlceraId(avaliacaoUlceraId: $avaliacaoUlceraId) { id avaliacaoUlceraId comprimento largura profundidade } }",
            variables = new {
                avaliacaoUlceraId
            }
        };
        var contentQuery = new StringContent(System.Text.Json.JsonSerializer.Serialize(queryMedida), System.Text.Encoding.UTF8, "application/json");
        var responseQuery = await _client.PostAsync("/graphql", contentQuery);
        responseQuery.EnsureSuccessStatusCode();
        var jsonQuery = await responseQuery.Content.ReadAsStringAsync();
        var dictQuery = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(jsonQuery, _jsonOptions);
        var medidaQuery = dictQuery["data"].GetProperty("medidaByAvaliacaoUlceraId");
        // Assert query
        medidaQuery.GetProperty("avaliacaoUlceraId").GetString().ShouldBe(avaliacaoUlceraId);
        medidaQuery.GetProperty("comprimento").GetDecimal().ShouldBe(10.5m);
        medidaQuery.GetProperty("largura").GetDecimal().ShouldBe(5.2m);
        medidaQuery.GetProperty("profundidade").GetDecimal().ShouldBe(1.1m);
    }
} 