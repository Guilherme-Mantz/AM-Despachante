using AMDespachante.Domain.Enums;

namespace AMDespachante.Application.ViewModels
{
    public class AtendimentoViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public TipoServico Servico { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorSaida { get; set; }
        public decimal Lucro => ValorEntrada - ValorSaida;
        public FormaPagamento FormaPagamento { get; set; }
        public string Observacoes { get; set; }
        public bool EstaPago { get; set; }

        public Guid ClienteId { get; set; }
        public ClienteViewModel Cliente { get; set; }

        public Guid? VeiculoId { get; set; }
        public VeiculoViewModel Veiculo { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
