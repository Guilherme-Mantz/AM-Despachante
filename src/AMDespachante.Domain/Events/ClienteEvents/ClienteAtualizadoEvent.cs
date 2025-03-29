using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.ClienteEvents
{
    public class ClienteAtualizadoEvent : Event
    {
        public Cliente Cliente { get; set; }

        public ClienteAtualizadoEvent(Cliente cliente)
        {
            AggregateId = Cliente.Id;
            Cliente = cliente;
        }
    }
}
