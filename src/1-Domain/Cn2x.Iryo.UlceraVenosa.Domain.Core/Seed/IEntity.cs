namespace Cn2x.Iryo.UlceraVenosa.Domain.Core {
    public interface IEntity<TIdentity> : IEvent, ISeed, IDesativar {
        TIdentity Id { get; set; }
    }
}
