using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Commands.RecursoCommands
{
    public class AtualizarRecursoCommand : Command
    {
        public AtualizarRecursoCommand(Guid id, string nome, string email, string cpf, bool ativo, CargoEnum cargo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Ativo = ativo;
            Cargo = cargo;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
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
