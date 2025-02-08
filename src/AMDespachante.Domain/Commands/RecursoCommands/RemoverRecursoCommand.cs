using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.RecursoCommands
{
    public class RemoverRecursoCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverRecursoCommand(Guid id)
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
