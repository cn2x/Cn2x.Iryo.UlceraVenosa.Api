using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class AvaliacaoUlceraRepository : BaseRepository<AvaliacaoUlcera>, IAvaliacaoUlceraRepository
{
    public AvaliacaoUlceraRepository(ApplicationDbContext context) : base(context)
    {
    }
    // Métodos específicos podem ser implementados aqui, se necessário
}
