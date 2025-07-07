using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Factories;

/// <summary>
/// Factory para criação de objetos Ulcera com validações
/// </summary>
public static class UlceraFactory
{
    /// <summary>
    /// Cria uma nova úlcera com todos os parâmetros obrigatórios
    /// </summary>
    public static Ulcera Create(
        Guid pacienteId,
        string duracao,
        DateTime dataExame,
        Clinica classeClinica,
        Etiologica etiologia,
        Anatomica anatomia,
        Patofisiologica patofisiologia,
        Caracteristicas? caracteristicas = null,
        SinaisInflamatorios? sinaisInflamatorios = null)
    {
        // Validações básicas
        if (pacienteId == Guid.Empty)
            throw new ArgumentException("PacienteId é obrigatório", nameof(pacienteId));
        
        if (string.IsNullOrWhiteSpace(duracao))
            throw new ArgumentException("Duração é obrigatória", nameof(duracao));
        
        if (dataExame == default)
            throw new ArgumentException("Data do exame é obrigatória", nameof(dataExame));

        // Criar o value object CEAP
        var ceap = new Ceap(classeClinica, etiologia, anatomia, patofisiologia);

        // Criar value objects padrão se não fornecidos
        var caracteristicasValue = caracteristicas ?? new Caracteristicas();
        var sinaisInflamatoriosValue = sinaisInflamatorios ?? new SinaisInflamatorios();

        return new Ulcera
        {
            PacienteId = pacienteId,
            Duracao = duracao,
            DataExame = dataExame,
            ClassificacaoCeap = ceap,
            Caracteristicas = caracteristicasValue,
            SinaisInflamatorios = sinaisInflamatoriosValue,
            Topografias = new List<Topografia>(),
            Exsudatos = new List<ExsudatoDaUlcera>(),
            Imagens = new List<ImagemUlcera>()
        };
    }

    /// <summary>
    /// Cria uma úlcera para atualização (mantém ID existente)
    /// </summary>
    public static Ulcera CreateForUpdate(
        Guid id,
        Guid pacienteId,
        string duracao,
        DateTime dataExame,
        Clinica classeClinica,
        Etiologica etiologia,
        Anatomica anatomia,
        Patofisiologica patofisiologia,
        Caracteristicas? caracteristicas = null,
        SinaisInflamatorios? sinaisInflamatorios = null)
    {
        var ulcera = Create(pacienteId, duracao, dataExame, classeClinica, etiologia, anatomia, patofisiologia, caracteristicas, sinaisInflamatorios);
        ulcera.Id = id;
        return ulcera;
    }
} 