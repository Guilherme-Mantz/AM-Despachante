using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Models
{
    public class Recurso : Entity, IAggregateRoot
    {
        public Recurso()
        {
        }

        public Recurso(string nome, string email, string cpf, string telefone, bool ativo, bool primeiroAcesso, CargoEnum cargo)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Telefone = telefone;
            Ativo = ativo;
            PrimeiroAcesso = primeiroAcesso;
            Cargo = cargo;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public bool Ativo { get; set; }
        public CargoEnum Cargo { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }

    }
}
