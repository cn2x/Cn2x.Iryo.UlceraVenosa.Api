using System;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Application.Features.AvaliacaoUlcera.Commands;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Tests.Application.Features.AvaliacaoUlcera;

public class UpsertAvaliacaoUlceraCommandTests
{
    [Fact]
    public void Deve_Criar_Comando_Com_Imagem_Base64()
    {
        // Arrange
        var ulceraId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataAvaliacao = DateTime.UtcNow;
        var mesesDuracao = 6;
        var arquivoBase64 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=";
        var descricaoImagem = "Imagem da úlcera venosa";
        var dataCapturaImagem = DateTime.UtcNow.AddHours(-1);

        // Act
        var comando = new UpsertAvaliacaoUlceraCommand
        {
            UlceraId = ulceraId,
            ProfissionalId = profissionalId,
            DataAvaliacao = dataAvaliacao,
            MesesDuracao = mesesDuracao,
            Arquivo = arquivoBase64,
            DescricaoImagem = descricaoImagem,
            DataCapturaImagem = dataCapturaImagem,
            Caracteristicas = new Caracteristicas
            {
                BordasDefinidas = true,
                TecidoGranulacao = false,
                Necrose = true,
                OdorFetido = false
            },
            SinaisInflamatorios = new SinaisInflamatorios
            {
                Calor = true,
                Rubor = false,
                Edema = true,
                Dor = 2,
                PerdadeFuncao = false,
                Eritema = true
            },
            Medida = new Medida
            {
                Comprimento = 5.5m,
                Largura = 3.2m,
                Profundidade = 1.0m
            }
        };

        // Assert
        Assert.Equal(ulceraId, comando.UlceraId);
        Assert.Equal(profissionalId, comando.ProfissionalId);
        Assert.Equal(dataAvaliacao, comando.DataAvaliacao);
        Assert.Equal(mesesDuracao, comando.MesesDuracao);
        Assert.Equal(arquivoBase64, comando.Arquivo);
        Assert.Equal(descricaoImagem, comando.DescricaoImagem);
        Assert.Equal(dataCapturaImagem, comando.DataCapturaImagem);
        Assert.NotNull(comando.Caracteristicas);
        Assert.True(comando.Caracteristicas.BordasDefinidas);
        Assert.False(comando.Caracteristicas.TecidoGranulacao);
        Assert.True(comando.Caracteristicas.Necrose);
        Assert.False(comando.Caracteristicas.OdorFetido);
        Assert.NotNull(comando.SinaisInflamatorios);
        Assert.True(comando.SinaisInflamatorios.Calor);
        Assert.False(comando.SinaisInflamatorios.Rubor);
        Assert.True(comando.SinaisInflamatorios.Edema);
        Assert.Equal(2, comando.SinaisInflamatorios.Dor);
        Assert.False(comando.SinaisInflamatorios.PerdadeFuncao);
        Assert.True(comando.SinaisInflamatorios.Eritema);
        Assert.NotNull(comando.Medida);
        Assert.Equal(5.5m, comando.Medida.Comprimento);
        Assert.Equal(3.2m, comando.Medida.Largura);
        Assert.Equal(1.0m, comando.Medida.Profundidade);
    }

    [Fact]
    public void Deve_Criar_Comando_Sem_Imagem()
    {
        // Arrange
        var ulceraId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataAvaliacao = DateTime.UtcNow;
        var mesesDuracao = 3;

        // Act
        var comando = new UpsertAvaliacaoUlceraCommand
        {
            UlceraId = ulceraId,
            ProfissionalId = profissionalId,
            DataAvaliacao = dataAvaliacao,
            MesesDuracao = mesesDuracao,
            Arquivo = null,
            DescricaoImagem = null,
            DataCapturaImagem = null,
            Caracteristicas = new Caracteristicas(),
            SinaisInflamatorios = new SinaisInflamatorios(),
            Medida = null
        };

        // Assert
        Assert.Equal(ulceraId, comando.UlceraId);
        Assert.Equal(profissionalId, comando.ProfissionalId);
        Assert.Equal(dataAvaliacao, comando.DataAvaliacao);
        Assert.Equal(mesesDuracao, comando.MesesDuracao);
        Assert.Null(comando.Arquivo);
        Assert.Null(comando.DescricaoImagem);
        Assert.Null(comando.DataCapturaImagem);
        Assert.NotNull(comando.Caracteristicas);
        Assert.NotNull(comando.SinaisInflamatorios);
        Assert.Null(comando.Medida);
    }

    [Fact]
    public void Deve_Criar_Comando_Com_Base64_Simples()
    {
        // Arrange
        var ulceraId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var arquivoBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg=="; // 1x1 pixel PNG

        // Act
        var comando = new UpsertAvaliacaoUlceraCommand
        {
            UlceraId = ulceraId,
            ProfissionalId = profissionalId,
            DataAvaliacao = DateTime.UtcNow,
            MesesDuracao = 1,
            Arquivo = arquivoBase64,
            Caracteristicas = new Caracteristicas(),
            SinaisInflamatorios = new SinaisInflamatorios()
        };

        // Assert
        Assert.Equal(ulceraId, comando.UlceraId);
        Assert.Equal(profissionalId, comando.ProfissionalId);
        Assert.Equal(arquivoBase64, comando.Arquivo);
        Assert.NotNull(comando.Caracteristicas);
        Assert.NotNull(comando.SinaisInflamatorios);
    }

    [Fact]
    public void Deve_Criar_Comando_Com_Base64_Com_Header()
    {
        // Arrange
        var ulceraId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var arquivoBase64 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=";

        // Act
        var comando = new UpsertAvaliacaoUlceraCommand
        {
            UlceraId = ulceraId,
            ProfissionalId = profissionalId,
            DataAvaliacao = DateTime.UtcNow,
            MesesDuracao = 2,
            Arquivo = arquivoBase64,
            Caracteristicas = new Caracteristicas(),
            SinaisInflamatorios = new SinaisInflamatorios()
        };

        // Assert
        Assert.Equal(ulceraId, comando.UlceraId);
        Assert.Equal(profissionalId, comando.ProfissionalId);
        Assert.Equal(arquivoBase64, comando.Arquivo);
        Assert.NotNull(comando.Caracteristicas);
        Assert.NotNull(comando.SinaisInflamatorios);
    }
}
