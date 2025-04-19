using AMDespachante.Domain.Commands.VeiculoCommands;
using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.ClienteCommands
{
    public class NovoClienteCommand : Command
    {
        public NovoClienteCommand() { }

        public string Nome { get; set; }
        public string DocumentoFiscal { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool EhEstacionamento { get; set; }
        public bool PagaMensalidade { get; set; }
        public decimal ValorMensalidade { get; set; }
        public DateTime? DataProximoVencimento { get; set; }
        public IList<NovoVeiculoCommand> Veiculos { get; set; }

        public override bool IsValid()
        {
            // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            // return ValidationResult.IsValid;

            return true;
        }
    }
}
