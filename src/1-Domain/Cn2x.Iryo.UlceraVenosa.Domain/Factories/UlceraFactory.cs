using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

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
        Guid avaliacaoId,
        string duracao,
        DateTime dataExame,
        decimal comprimentoCm,
        decimal largura,
        decimal profundidade,
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
        
        if (avaliacaoId == Guid.Empty)
            throw new ArgumentException("AvaliacaoId é obrigatório", nameof(avaliacaoId));
        
        if (string.IsNullOrWhiteSpace(duracao))
            throw new ArgumentException("Duração é obrigatória", nameof(duracao));
        
        if (dataExame == default)
            throw new ArgumentException("Data do exame é obrigatória", nameof(dataExame));
        
        if (comprimentoCm <= 0)
            throw new ArgumentException("Comprimento deve ser maior que zero", nameof(comprimentoCm));
        
        if (largura <= 0)
            throw new ArgumentException("Largura deve ser maior que zero", nameof(largura));
        
        if (profundidade < 0)
            throw new ArgumentException("Profundidade não pode ser negativa", nameof(profundidade));

        // Criar o value object CEAP
        var ceap = new Ceap(classeClinica, etiologia, anatomia, patofisiologia);

        // Criar value objects padrão se não fornecidos
        var caracteristicasValue = caracteristicas ?? new Caracteristicas();
        var sinaisInflamatoriosValue = sinaisInflamatorios ?? new SinaisInflamatorios();

        return new Ulcera
        {
            PacienteId = pacienteId,
            AvaliacaoId = avaliacaoId,
            Duracao = duracao,
            DataExame = dataExame,
            ComprimentoCm = comprimentoCm,
            Largura = largura,
            Profundidade = profundidade,
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
        Guid avaliacaoId,
        string duracao,
        DateTime dataExame,
        decimal comprimentoCm,
        decimal largura,
        decimal profundidade,
        Clinica classeClinica,
        Etiologica etiologia,
        Anatomica anatomia,
        Patofisiologica patofisiologia,
        Caracteristicas? caracteristicas = null,
        SinaisInflamatorios? sinaisInflamatorios = null)
    {
        var ulcera = Create(pacienteId, avaliacaoId, duracao, dataExame, comprimentoCm, largura, profundidade, 
                           classeClinica, etiologia, anatomia, patofisiologia, caracteristicas, sinaisInflamatorios);
        
        ulcera.Id = id;
        return ulcera;
    }
} 