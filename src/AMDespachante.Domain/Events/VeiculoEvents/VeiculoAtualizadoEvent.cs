using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.VeiculoEvents
{
    public class VeiculoAtualizadoEvent : Event
    {
        public Veiculo Veiculo { get; set; }

        public VeiculoAtualizadoEvent(Veiculo veiculo)
        {
            AggregateId = veiculo.Id;
            Veiculo = veiculo;
        }
    }
}
