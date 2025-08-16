using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Tests.Domain;

public class WhatsappTest
{
    [Fact]
    public void IsValid_VerificarSeTelefoneEValidado_Corretamente()
    {
        //Arrange
        const string validNumber = "5511978866442";
        //Act
        Whatsapp newWhatsapp = new(validNumber);
        //Assert
        Assert.Equal(validNumber, newWhatsapp.GetNumber());
    }
    
    [Fact]
    public void IsValid_VerificarSeTelefoneInvalidoEValidado_Corretamente()
    {
        //Arrange
        const string InvalidNumber = "734734982732974";
        const string InValidDDD = "5520978866442";
        const string InValidDDI = "5611978866442";
        //Act
        Whatsapp newWhatsapp = new(InvalidNumber);
        Whatsapp newWhatsappDDDInvalid = new(InValidDDD);
        Whatsapp newWhatsappDDIInvalid = new(InValidDDI);
        //Assert
        Assert.Equal(String.Empty, newWhatsapp.GetNumber()); //Numero Invalido
        Assert.Equal(String.Empty, newWhatsappDDDInvalid.GetNumber()); // DDD Invalido
        Assert.Equal(String.Empty, newWhatsappDDIInvalid.GetNumber()); // DDI Invalido
    }
}