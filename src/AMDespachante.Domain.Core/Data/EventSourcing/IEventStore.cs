using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Core.Data.EventSourcing
{
    public interface IEventStore
    {
        void Store<T>(T theEvent) where T : Event;
        void Store(StoredEvent storedEvent);
    }
}
