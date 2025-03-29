using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Events.VeiculoEvents
{
    public class VeiculoRemovidoEvent : Event
    {
        public Guid Id { get; set; }

        public VeiculoRemovidoEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
