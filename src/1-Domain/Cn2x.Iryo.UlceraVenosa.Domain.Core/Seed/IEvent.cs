using MediatR;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core
{
    public interface IDesativar { 
        bool Desativada { get; set; }
    }

    public interface IEvent
    {
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
        IReadOnlyCollection<INotification>? DomainEvents { get; }
    }
}
