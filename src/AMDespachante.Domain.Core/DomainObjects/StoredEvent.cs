

using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Core.DomainObjects
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event @event, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = user;
        }

        public StoredEvent(Event @event, string data)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = string.Empty;
        }
        public StoredEvent(Guid aggregateId, string messageType, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            MessageType = messageType;
            Data = data;
            User = user;
        }

        // EF
        protected StoredEvent() { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
