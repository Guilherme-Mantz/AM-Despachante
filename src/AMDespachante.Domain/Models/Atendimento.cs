using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMDespachante.Domain.Models
{
    public class Atendimento : Entity, IAggregateRoot
    {
        public Atendimento() { }

        public Atendimento(
            DateTime data, 
            TipoServicoEnum servico, 
            decimal valorEntrada,
            decimal valorSaida, 
            FormaPagamento formaPagamento, 
            string observacoes, 
            bool estaPago,
            StatusAtendimentoEnum status,
            Guid clienteId,
            Guid? veiculoId, 
            string numeroATPV, 
            string numeroCRLV)
        {
            Data = data;
            Servico = servico;
            ValorEntrada = valorEntrada;
            ValorSaida = valorSaida;
            FormaPagamento = formaPagamento;
            Observacoes = observacoes;
            EstaPago = estaPago;
            Status = status;
            ClienteId = clienteId;
            VeiculoId = veiculoId;
            NumeroATPV = numeroATPV;
            NumeroCRLV = numeroCRLV;
        }

        public DateTime Data { get; set; }
        public TipoServicoEnum Servico { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorEntrada { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorSaida { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public string Observacoes { get; set; }
        public bool EstaPago { get; set; }
        public StatusAtendimentoEnum Status { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Guid? VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public string NumeroATPV { get; set; }
        public string NumeroCRLV { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
