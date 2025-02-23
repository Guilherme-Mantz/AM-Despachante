using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Commands.RecursoCommands
{
    public class AtualizarRecursoCommand : Command
    {
        public AtualizarRecursoCommand()
        {
            
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public CargoEnum Cargo { get; set; }

        public override bool IsValid()
        {
            //ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
            //return ValidationResult.IsValid;

            return true;
        }
    }
}
