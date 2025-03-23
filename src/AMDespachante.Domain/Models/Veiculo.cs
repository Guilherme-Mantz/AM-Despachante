using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Models
{
    public class Veiculo : Entity
    {
        public Veiculo() { }

        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }

        // 1:N com Atendimento
        public virtual ICollection<Atendimento> Atendimentos { get; set; }
    }
}
