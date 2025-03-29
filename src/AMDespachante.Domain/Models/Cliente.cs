using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente() { }

        public Cliente(
            string nome, 
            string cpf,
            string telefone,
            string email, 
            bool ehEstacionamento, 
            bool pagaMensalidade,
            decimal valorMensalidade, 
            DateTime? dataProximoVencimento, 
            ICollection<Veiculo> veiculos)
        {
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
            EhEstacionamento = ehEstacionamento;
            PagaMensalidade = pagaMensalidade;
            ValorMensalidade = valorMensalidade;
            DataProximoVencimento = dataProximoVencimento;
            Veiculos = veiculos;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool EhEstacionamento { get; set; }
        public bool PagaMensalidade { get; set; }
        public decimal ValorMensalidade { get; set; }
        public DateTime? DataProximoVencimento { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }

        // 1:N com Veículo
        public virtual ICollection<Veiculo> Veiculos { get; set; }

        // 1:N com Atendimento
        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        // 1:N com Mensalidade
        public virtual ICollection<Mensalidade> Mensalidades { get; set; }
    }
}
