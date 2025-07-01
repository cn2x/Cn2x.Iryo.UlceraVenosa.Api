using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

/// <summary>
/// Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology) como Value Object
/// </summary>
public class Ceap : ValueObject
{
    public Clinica ClasseClinica { get; private set; }
    public Etiologica Etiologia { get; private set; }
    public Anatomica Anatomia { get; private set; }
    public Patofisiologica Patofisiologia { get; private set; }
    
    public Ceap(Clinica classeClinica, Etiologica etiologia, Anatomica anatomia, Patofisiologica patofisiologia)
    {
        // Validação de entrada - todos os campos são obrigatórios
        if (classeClinica == null)
            throw new ArgumentException("Classe clínica é obrigatória", nameof(classeClinica));
        
        if (etiologia == null)
            throw new ArgumentException("Etiologia é obrigatória", nameof(etiologia));
        
        if (anatomia == null)
            throw new ArgumentException("Anatomia é obrigatória", nameof(anatomia));
        
        if (patofisiologia == null)
            throw new ArgumentException("Patofisiologia é obrigatória", nameof(patofisiologia));

        ClasseClinica = classeClinica;
        Etiologia = etiologia;
        Anatomia = anatomia;
        Patofisiologia = patofisiologia;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ClasseClinica;
        yield return Etiologia;
        yield return Anatomia;
        yield return Patofisiologia;
    }

    public override string ToString()
    {
        return $"{ClasseClinica.Name}{Etiologia.Name}{Anatomia.Name}{Patofisiologia.Name}";
    }
} 