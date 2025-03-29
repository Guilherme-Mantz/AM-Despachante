using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.ClienteCommands
{
    public class RemoverClienteCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverClienteCommand(Guid id)
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
