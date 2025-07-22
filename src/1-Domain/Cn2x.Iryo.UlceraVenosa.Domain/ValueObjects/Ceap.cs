using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using System;
using System.Collections.Generic;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

/// <summary>
/// Classificação CEAP (Clinical-Etiology-Anatomy-Pathophysiology) como Value Object
/// </summary>
public class Ceap : ValueObject
{
    public Clinica ClasseClinica { get; private set; } = null!;
    public Etiologica Etiologia { get; private set; } = null!;
    public Anatomica Anatomia { get; private set; } = null!;
    public Patofisiologica Patofisiologia { get; private set; } = null!;
    
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

    // Construtor padrão para o EF Core
    private Ceap() { }

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

    public static Ceap? FromInput(object? inputObj)
    {
        if (inputObj == null) return null;
        // Cast seguro
        var input = inputObj as dynamic;
        var clinica = input.ClasseClinica switch
        {
            ClinicaEnum.SemSinais => Clinica.SemSinais,
            ClinicaEnum.Telangiectasias => Clinica.Telangiectasias,
            ClinicaEnum.Varizes => Clinica.Varizes,
            ClinicaEnum.Edema => Clinica.Edema,
            ClinicaEnum.PigmentacaoOuEczema => Clinica.PigmentacaoOuEczema,
            ClinicaEnum.LipodermatoescleroseOuAtrofia => Clinica.LipodermatoescleroseOuAtrofia,
            ClinicaEnum.UlceraCicatrizada => Clinica.UlceraCicatrizada,
            ClinicaEnum.UlceraAtiva => Clinica.UlceraAtiva,
            _ => null
        };
        var etiologica = input.Etiologia switch
        {
            EtiologicaEnum.Congenita => Etiologica.Congenita,
            EtiologicaEnum.Primaria => Etiologica.Primaria,
            EtiologicaEnum.Secundaria => Etiologica.Secundaria,
            EtiologicaEnum.NaoIdentificada => Etiologica.NaoIdentificada,
            _ => null
        };
        var anatomica = input.Anatomia switch
        {
            AnatomicaEnum.Superficial => Anatomica.Superficial,
            AnatomicaEnum.Profundo => Anatomica.Profundo,
            AnatomicaEnum.Perfurante => Anatomica.Perfurante,
            AnatomicaEnum.NaoIdentificada => Anatomica.NaoIdentificada,
            _ => null
        };
        var patofisiologica = input.Patofisiologia switch
        {
            PatofisiologicaEnum.Refluxo => Patofisiologica.Refluxo,
            PatofisiologicaEnum.Obstrucao => Patofisiologica.Obstrucao,
            PatofisiologicaEnum.NaoIdentificada => Patofisiologica.NaoIdentificada,
            _ => null
        };
        return new Ceap(clinica!, etiologica!, anatomica!, patofisiologica!);
    }
}