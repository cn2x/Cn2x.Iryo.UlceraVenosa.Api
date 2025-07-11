using System;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Factories;

public class UlceraFactoryTests
{
    [Fact]
    public void Create_DeveCriarUlceraComTopografiaPerna()
    {
        // Arrange
        var pacienteId = Guid.NewGuid();
        var topografia = new TopografiaPerna
        {
            Id = Guid.NewGuid(),
            LateralidadeId = Guid.NewGuid(),
            Lateralidade = new Lateralidade { Id = Guid.NewGuid(), Nome = "Direita" },
            SegmentacaoId = Guid.NewGuid(),
            Segmentacao = new Segmentacao { Id = Guid.NewGuid(), Sigla = "TS", Descricao = "desc" },
            RegiaoAnatomicaId = Guid.NewGuid(),
            RegiaoAnatomica = new RegiaoAnatomica { Id = Guid.NewGuid(), Sigla = "M", Descricao = "Medial" }
        };

        // Act
        var ulcera = UlceraFactory.Create(pacienteId, topografia);

        // Assert
        Assert.NotNull(ulcera);
        Assert.Equal(pacienteId, ulcera.PacienteId);
        Assert.Equal(topografia, ulcera.Topografia);
    }

    [Fact]
    public void Create_DeveCriarUlceraComTopografiaPe()
    {
        // Arrange
        var pacienteId = Guid.NewGuid();
        var topografia = new TopografiaPe
        {
            Id = Guid.NewGuid(),
            LateralidadeId = Guid.NewGuid(),
            Lateralidade = new Lateralidade { Id = Guid.NewGuid(), Nome = "Esquerda" },
            RegiaoTopograficaPeId = Guid.NewGuid(),
            RegiaoTopograficaPe = new RegiaoTopograficaPe { Id = Guid.NewGuid(), Sigla = "DOR", Descricao = "Dorsal" }
        };

        // Act
        var ulcera = UlceraFactory.Create(pacienteId, topografia);

        // Assert
        Assert.NotNull(ulcera);
        Assert.Equal(pacienteId, ulcera.PacienteId);
        Assert.Equal(topografia, ulcera.Topografia);
    }
}
