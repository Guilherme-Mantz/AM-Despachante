using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Events.AtendimentoEvents
{
    public class AtendimentoCriadoEvent : Event
    {
        public Atendimento Atendimento { get; set; }

        public AtendimentoCriadoEvent(Atendimento atendimento)
        {
            AggregateId = atendimento.Id;
            Atendimento = atendimento;
        }
    }
}
