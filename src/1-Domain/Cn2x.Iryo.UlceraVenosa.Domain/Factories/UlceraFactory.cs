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
        Ceap? classificacaoCeap = null)
    {
        if (pacienteId == Guid.Empty)
            throw new ArgumentException("PacienteId é obrigatório", nameof(pacienteId));

        var ulcera = new Ulcera
        {
            PacienteId = pacienteId,
            Topografias = new List<Topografia>()
        };
        if (classificacaoCeap != null)
            ulcera.ClassificacaoCeap = classificacaoCeap;
        return ulcera;
    }

    /// <summary>
    /// Cria uma úlcera para atualização (mantém ID existente)
    /// </summary>
    public static Ulcera CreateForUpdate(
        Guid id,
        Guid pacienteId,
        Ceap? classificacaoCeap = null)
    {
        var ulcera = Create(pacienteId, classificacaoCeap);
        ulcera.Id = id;
        return ulcera;
    }
}