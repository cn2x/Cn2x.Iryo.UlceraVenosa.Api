using HotChocolate;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core {

    [ExcludeFromCodeCoverage]
    public abstract class Entity<TIdentity> : IEntity<TIdentity>, ISeed {
        int? _requestedHashCode;

        public TIdentity Id { get; set; }

        private List<INotification>? _domainEvents;

        [GraphQLIgnore]
        public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id!.Equals(default(TIdentity));
        }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity<TIdentity>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity<TIdentity> item = (Entity<TIdentity>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity<TIdentity>? left, Entity<TIdentity>? right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right)
        {
            return !(left == right);
        }

        public bool Desativada { get; set; } = false; 
    }
}
