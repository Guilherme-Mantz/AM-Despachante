using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.AtendimentoEvents
{
    public class AtendimentoAtualizadoEvent : Event
    {
        public Atendimento Atendimento { get; set; }

        public AtendimentoAtualizadoEvent(Atendimento atendimento)
        {
            AggregateId = atendimento.Id;
            Atendimento = atendimento;
        }
    }
}
