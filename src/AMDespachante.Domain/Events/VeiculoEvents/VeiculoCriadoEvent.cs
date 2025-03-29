using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.VeiculoEvents
{
    public class VeiculoCriadoEvent : Event
    {
        public Veiculo Veiculo { get; set; }

        public VeiculoCriadoEvent(Veiculo veiculo)
        {
            AggregateId = veiculo.Id;
            Veiculo = veiculo;
        }
    }
}
