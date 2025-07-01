using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Entities;

/// <summary>
/// Tipo de exsudato (agora Exsudato)
/// </summary>
public class Exsudato : Entity<Guid>, IAggregateRoot
{
    public string Descricao { get; set; } = string.Empty;
    
    // Navegação
    public virtual ICollection<ExsudatoDaUlcera> ExsudatosDaUlcera { get; set; } = new List<ExsudatoDaUlcera>();
} 