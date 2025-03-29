using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.VeiculoCommands
{
    public class RemoverVeiculoCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverVeiculoCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            // return ValidationResult.IsValid;

            return true;
        }
    }
}
