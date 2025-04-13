using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Events.AtendimentoEvents
{
    public class AtendimentoRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public AtendimentoRemovidoEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
