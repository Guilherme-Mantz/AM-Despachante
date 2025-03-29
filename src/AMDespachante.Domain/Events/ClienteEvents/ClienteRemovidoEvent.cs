using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Events.ClienteEvents
{
    public class ClienteRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public ClienteRemovidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
