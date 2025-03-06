using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.RecursoCommands
{
    public class DesativarPrimeiroAcessoRecursoCommand : Command
    {
        public string Cpf { get; set; }

        public DesativarPrimeiroAcessoRecursoCommand(string cpf)
        {
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            // return ValidationResult.IsValid;

            return true;
        }
    }
}
