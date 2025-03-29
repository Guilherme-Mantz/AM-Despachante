using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.ClienteEvents
{
    public class ClienteCriadoEvent : Event
    {
        public Cliente Cliente { get; set; }

        public ClienteCriadoEvent(Cliente cliente)
        {
            AggregateId = cliente.Id;
            Cliente = cliente;
        }
    }
}
