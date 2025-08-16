using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Tests.Domain;

public class EmailTest
{
    [Fact]
    public void IsValid_DeveValidarEmail_Corretamente()
    {
        //Arrange
        const string fakeEmail = "jurandirtabua@example.com";
        //Act
        Email newEmail = new(fakeEmail);
        //Assert
        Assert.Equal(fakeEmail, newEmail.GetAddress());
    }
    
    [Fact]
    public void IsValid_DeveValidarEmailInvalido_Corretamente()
    {
        //Arrange
        const string fakeEmail1 = "jurandirtabua@examplecom";
        const string fakeEmail2 = "jurandirtabuaexample.com";
        const string fakeEmail3 = "jurandirtabua@@examplecom";
        //Act
        Email newEmail1= new(fakeEmail1);
        Email newEmail2 = new(fakeEmail2);
        Email newEmail3 = new(fakeEmail3);
        //Assert
        Assert.Equal(String.Empty, newEmail1.GetAddress());
        Assert.Equal(String.Empty, newEmail2.GetAddress());
        Assert.Equal(String.Empty, newEmail3.GetAddress());
    }
}