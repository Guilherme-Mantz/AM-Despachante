using AMDespachante.Domain.Core.Data.EventSourcing;
using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.EventSourcing.Context;
using Microsoft.EntityFrameworkCore;

namespace AMDespachante.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly EventStoreSqlContext _context;

        public EventSourcingRepository(EventStoreSqlContext context)
        {
            _context = context;
        }

        public async Task<IList<StoredEvent>> All(Guid aggregateId)
        {
            return await (from e in _context.StoredEvent where e.AggregateId == aggregateId select e).ToListAsync();
        }

        public void Store(StoredEvent @event)
        {
            _context.StoredEvent.Add(@event);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
