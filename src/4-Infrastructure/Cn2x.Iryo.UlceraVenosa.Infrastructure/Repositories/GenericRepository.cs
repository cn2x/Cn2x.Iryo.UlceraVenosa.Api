using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

public class GenericRepository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public GenericRepository(ApplicationDbContext context) : base(context) { }
} 