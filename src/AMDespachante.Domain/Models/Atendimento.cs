using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Models
{
    public class Atendimento : Entity, IAggregateRoot
    {
        public Atendimento() { }

        public DateTime Data { get; set; }
        public TipoServico Servico { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorSaida { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public string Observacoes { get; set; }
        public bool EstaPago { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Guid? VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
