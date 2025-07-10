using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data {

    public partial class ApplicationDbContext {
        public async Task<Topografia?> FindTipografiaFilterByTypeAsync(CancellationToken cancellationToken, int id, int tipo) {
            var peFinder = new TopografiaPeFinder();
            var pernaFinder = new TopografiaPernaFinder();            
            return await pernaFinder.FindAsync(this, id, tipo, cancellationToken, peFinder);
        }
    }
}
