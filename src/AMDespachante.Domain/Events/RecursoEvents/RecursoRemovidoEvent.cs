using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public RecursoRemovidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
