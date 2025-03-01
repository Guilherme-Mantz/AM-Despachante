using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoRemovidoEvent : Event
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }

        public RecursoRemovidoEvent(Guid id, string cpf)
        {
            Id = id;
            AggregateId = id;
            Cpf = cpf;
        }
    }
}
