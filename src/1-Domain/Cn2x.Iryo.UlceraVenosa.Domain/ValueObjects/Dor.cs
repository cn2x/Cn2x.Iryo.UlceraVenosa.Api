using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

public class Dor : ValueObject {
    public Intensidade Intensidade { get; }

    public Dor(Intensidade intensidade) {
        Intensidade = intensidade;
    }

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return Intensidade;
    }
        
    public static implicit operator Dor(Intensidade intensidade) => new Dor(intensidade);
    public static implicit operator Intensidade(Dor dor) => dor.Intensidade;
    public static implicit operator Dor(int intensidade) {

        var exists = Enum.GetValues<Intensidade>().Any(x => (int)x == intensidade);
        if(exists) return  new Dor((Intensidade)intensidade);

        return Intensidade.Zero;
    }
}
