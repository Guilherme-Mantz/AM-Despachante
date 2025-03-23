using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Models
{
    public class Mensalidade : Entity
    {
        public Mensalidade() { }

        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public bool EstaPago { get; set; }
        public string Observacoes { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
