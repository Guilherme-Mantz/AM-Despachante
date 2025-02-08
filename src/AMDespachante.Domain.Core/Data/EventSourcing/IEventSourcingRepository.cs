using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
