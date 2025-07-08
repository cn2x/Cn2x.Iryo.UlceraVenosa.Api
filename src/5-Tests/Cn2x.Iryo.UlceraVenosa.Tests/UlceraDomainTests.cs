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
        var ulcera = new Ulcera { PacienteId = pacienteId, ClassificacaoCeap = ceap };

        // Assert
        Assert.Equal(pacienteId, ulcera.PacienteId);
        Assert.Equal(ceap, ulcera.ClassificacaoCeap);
    }
}
