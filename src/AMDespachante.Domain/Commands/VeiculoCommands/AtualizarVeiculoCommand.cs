using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Commands.VeiculoCommands
{
    public class AtualizarVeiculoCommand : Command
    {
        public AtualizarVeiculoCommand() { }

        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public TipoVeiculoEnum TipoVeiculo { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }
        public Guid ClienteId { get; set; }

        public override bool IsValid()
        {
            // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            // return ValidationResult.IsValid;

            return true;
        }
    }
}
