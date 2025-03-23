using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Models
{
    public class Cliente : Entity
    {
        public Cliente() { }

        public string Nome { get; set; }
        public string CPF { get; set; }
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
