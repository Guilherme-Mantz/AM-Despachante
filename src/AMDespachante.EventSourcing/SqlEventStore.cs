using AMDespachante.Domain.Core.Data.EventSourcing;
using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Core.User;
using Newtonsoft.Json;

namespace AMDespachante.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventSourcingRepository _eventStoreRepository;
        private readonly IAppUser _user;

        public SqlEventStore(IEventSourcingRepository eventStoreRepository, IAppUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Store<T>(T @event) where T : Event
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(@event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                var storedEvent = new StoredEvent(
                    @event,
                    serialized,
                    _user.Name
                    );

                _eventStoreRepository.Store(storedEvent);
            }
            catch (System.Exception ex)
            {

                var storedEvent = new StoredEvent(
                    @event,
                    $"Error {ex.Message}",
                    _user.Name
                    );

                _eventStoreRepository.Store(storedEvent);
            }
        }

        public void Store(StoredEvent storedEvent)
        {
            _eventStoreRepository.Store(storedEvent);
        }
    }
}
