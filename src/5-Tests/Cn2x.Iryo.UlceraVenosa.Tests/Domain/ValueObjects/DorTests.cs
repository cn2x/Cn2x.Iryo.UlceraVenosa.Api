using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Xunit;

namespace Cn2x.Iryo.UlceraVenosa.Tests.Domain.ValueObjects;

public class DorTests
{
    [Fact]
    public void Dor_DeveCriarComIntensidade()
    {
        // Arrange
        var intensidade = Intensidade.Cinco;

        // Act
        var dor = new Dor(intensidade);

        // Assert
        Assert.Equal(Intensidade.Cinco, dor.Intensidade);
    }

    [Fact]
    public void Dor_DeveSerIgualQuandoIntensidadeForIgual()
    {
        // Arrange
        var dor1 = new Dor(Intensidade.Tres);
        var dor2 = new Dor(Intensidade.Tres);

        // Act & Assert
        Assert.Equal(dor1, dor2);
        Assert.Equal(dor1.GetHashCode(), dor2.GetHashCode());
    }

    [Fact]
    public void Dor_DeveSerDiferenteQuandoIntensidadeForDiferente()
    {
        // Arrange
        var dor1 = new Dor(Intensidade.Um);
        var dor2 = new Dor(Intensidade.Dez);

        // Act & Assert
        Assert.NotEqual(dor1, dor2);
    }

    [Theory]
    [InlineData(Intensidade.Zero)]
    [InlineData(Intensidade.Um)]
    [InlineData(Intensidade.Cinco)]
    [InlineData(Intensidade.Dez)]
    public void Dor_DeveConverterImplicitamenteDeIntensidade(Intensidade intensidade)
    {
        // Act
        Dor dor = intensidade;

        // Assert
        Assert.Equal(intensidade, dor.Intensidade);
    }

    [Theory]
    [InlineData(Intensidade.Zero)]
    [InlineData(Intensidade.Tres)]
    [InlineData(Intensidade.Sete)]
    [InlineData(Intensidade.Dez)]
    public void Dor_DeveConverterImplicitamenteParaIntensidade(Intensidade intensidade)
    {
        // Arrange
        var dor = new Dor(intensidade);

        // Act
        Intensidade resultado = dor;

        // Assert
        Assert.Equal(intensidade, resultado);
    }

    [Theory]
    [InlineData(0, Intensidade.Zero)]
    [InlineData(1, Intensidade.Um)]
    [InlineData(5, Intensidade.Cinco)]
    [InlineData(10, Intensidade.Dez)]
    public void Dor_DeveConverterImplicitamenteDeInteiroValido(int valor, Intensidade intensidadeEsperada)
    {
        // Act
        Dor dor = valor;

        // Assert
        Assert.Equal(intensidadeEsperada, dor.Intensidade);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(100)]
    public void Dor_DeveRetornarZeroQuandoInteiroInvalido(int valorInvalido)
    {
        // Act
        Dor dor = valorInvalido;

        // Assert
        Assert.Equal(Intensidade.Zero, dor.Intensidade);
    }

    [Fact]
    public void Intensidade_DeveTerTodosOsValoresCorretos()
    {
        // Assert
        Assert.Equal(0, (byte)Intensidade.Zero);
        Assert.Equal(1, (byte)Intensidade.Um);
        Assert.Equal(2, (byte)Intensidade.Dois);
        Assert.Equal(3, (byte)Intensidade.Tres);
        Assert.Equal(4, (byte)Intensidade.Quatro);
        Assert.Equal(5, (byte)Intensidade.Cinco);
        Assert.Equal(6, (byte)Intensidade.Seis);
        Assert.Equal(7, (byte)Intensidade.Sete);
        Assert.Equal(8, (byte)Intensidade.Oito);
        Assert.Equal(9, (byte)Intensidade.Nove);
        Assert.Equal(10, (byte)Intensidade.Dez);
    }
}
