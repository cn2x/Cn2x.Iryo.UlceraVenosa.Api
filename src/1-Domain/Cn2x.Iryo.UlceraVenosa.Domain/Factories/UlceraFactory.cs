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
        Ceap? classificacaoCeap = null,
        IEnumerable<SegmentoEnum>? segmentos = null)
    {
        if (pacienteId == Guid.Empty)
            throw new ArgumentException("PacienteId é obrigatório", nameof(pacienteId));

        var ulcera = new Ulcera
        {
            PacienteId = pacienteId,
            Segmentos = segmentos != null ? segmentos.Select(s => new Segmento { Tipo = s }).ToList() : new List<Segmento>()
        };
        if (classificacaoCeap != null)
            ulcera.Ceap = classificacaoCeap;
        return ulcera;
    }

    /// <summary>
    /// Cria uma úlcera para atualização (mantém ID existente)
    /// </summary>
    public static Ulcera CreateForUpdate(
        Guid id,
        Guid pacienteId,
        Ceap? classificacaoCeap = null,
        IEnumerable<SegmentoEnum>? segmentos = null)
    {
        var ulcera = Create(pacienteId, classificacaoCeap, segmentos);
        ulcera.Id = id;
        return ulcera;
    }
}