using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.AtendimentoCommands
{
    public class RemoverAtendimentoCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverAtendimentoCommand(Guid id)
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
