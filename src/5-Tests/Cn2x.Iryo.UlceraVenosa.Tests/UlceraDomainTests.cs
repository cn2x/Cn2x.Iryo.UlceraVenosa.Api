using System;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Tests;

public class UlceraDomainTests
{
    [Fact]
    public void Deve_Criar_Ulcera_Com_Ceap_Valido()
    {
        // Arrange
        var ceap = new Ceap(
            Clinica.FromValue<Clinica>(ClinicaEnum.UlceraCicatrizada),
            Etiologica.FromValue<Etiologica>(EtiologicaEnum.NaoIdentificada),
            Anatomica.FromValue<Anatomica>(AnatomicaEnum.Superficial),
            Patofisiologica.FromValue<Patofisiologica>(PatofisiologicaEnum.Refluxo)
        );

        var pacienteId = Guid.NewGuid();
        var ulcera = new Ulcera { PacienteId = pacienteId, Ceap = ceap };

        // Assert
        Assert.Equal(pacienteId, ulcera.PacienteId);
        Assert.Equal(ceap, ulcera.Ceap);
    }

    [Fact]
    public void Deve_Criar_Ulcera_Com_Segmentos() {
        // Arrange
        var ceap = new Ceap(
            Clinica.FromValue<Clinica>(ClinicaEnum.UlceraAtiva),
            Etiologica.FromValue<Etiologica>(EtiologicaEnum.Primaria),
            Anatomica.FromValue<Anatomica>(AnatomicaEnum.Profundo),
            Patofisiologica.FromValue<Patofisiologica>(PatofisiologicaEnum.Obstrucao)
        );

        var pacienteId = Guid.NewGuid();
        var ulceraId = Guid.NewGuid();
        var segmento1 = new Segmento { Id = Guid.NewGuid(), Tipo = SegmentoEnum.TercoSuperior };
        var segmento2 = new Segmento { Id = Guid.NewGuid(), Tipo = SegmentoEnum.TercoInferior };

        var ulcera = new Ulcera {
            Id = ulceraId,
            PacienteId = pacienteId,
            Ceap = ceap,
            Segmentos = new[] { segmento1, segmento2 }
        };

        // Assert
        Assert.Equal(pacienteId, ulcera.PacienteId);
        Assert.Equal(ceap, ulcera.Ceap);
        Assert.Equal(2, ulcera.Segmentos.Count);
        Assert.Contains(segmento1, ulcera.Segmentos);
        Assert.Contains(segmento2, ulcera.Segmentos);
    }
}
