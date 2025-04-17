using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Commands.AtendimentoCommands
{
    public class AtualizarAtendimentoCommand : Command
    {
        public AtualizarAtendimentoCommand()
        {
            
        }

        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public TipoServicoEnum Servico { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorSaida { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public string Observacoes { get; set; }
        public bool EstaPago { get; set; }
        public StatusAtendimentoEnum Status { get; set; }
        public Guid ClienteId { get; set; }
        public Guid? VeiculoId { get; set; }
        public string NumeroATPV { get; set; }
        public string NumeroCRLV { get; set; }

        public override bool IsValid()
        {
            // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            // return ValidationResult.IsValid;

            return true;
        }
    }
}
