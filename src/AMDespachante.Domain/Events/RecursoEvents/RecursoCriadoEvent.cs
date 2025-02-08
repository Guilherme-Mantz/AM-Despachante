using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoCriadoEvent : Event
    {
        public Recurso Recurso { get; set; }

        public RecursoCriadoEvent(Recurso recurso)
        {
            Recurso = recurso;
            AggregateId = recurso.Id;
        }
    }
}
