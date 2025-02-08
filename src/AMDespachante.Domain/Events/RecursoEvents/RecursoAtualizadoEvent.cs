using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoAtualizadoEvent : Event
    {
        public Recurso Recurso { get; set; }

        public RecursoAtualizadoEvent(Recurso recurso)
        {
            Recurso = recurso;
            AggregateId = recurso.Id;
        }
    }
}
