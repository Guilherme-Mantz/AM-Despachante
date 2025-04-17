using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
