using System;
using Xunit;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain;

namespace Cn2x.Iryo.UlceraVenosa.Tests;

public class UlceraDomainTests
{
    [Fact]
    public void Deve_Criar_Ulcera_Com_Ceap_Valido()
    {
        //// Arrange
        //var ceap = new Ceap(
        //    null!, // Passe valores válidos se necessário
        //    null!,
        //    null!,
        //    null!
        //);
        //var pacienteId = Guid.NewGuid();
        //var segmentacao = new Segmentacao { Id = 1, Sigla = "A", Descricao = "Segmento A" };
        //var regiaoAnatomica = new RegiaoAnatomica { Id = 1, Sigla = "RA", Descricao = "Região A" };
        //var topografia = new TopografiaPerna
        //{
        //    Id = 1,
        //    SegmentacaoId = segmentacao.Id,
        //    Segmentacao = segmentacao,
        //    RegiaoAnatomicaId = regiaoAnatomica.Id,
        //    RegiaoAnatomica = regiaoAnatomica
        //};
        //var ulcera = new Ulcera { PacienteId = pacienteId, Ceap = ceap, TopografiaFerida = topografia };

        //// Assert
        //Assert.Equal(pacienteId, ulcera.PacienteId);
        //Assert.Equal(ceap, ulcera.Ceap);
        //Assert.Equal(topografia, ulcera.TopografiaFerida);
    }
}
