using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Commands.RecursoCommands
{
    public class NovoRecursoCommand : Command
    {
        public NovoRecursoCommand(string nome, string email, string cpf, bool ativo, CargoEnum cargo)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Ativo = ativo;
            Cargo = cargo;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public CargoEnum Cargo { get; set; }

        public override bool IsValid()
        {
           // ValidationResult = new NovoAcessorioCommandValidation().Validate(this);
           // return ValidationResult.IsValid;

            return true;
        }

    }
}
