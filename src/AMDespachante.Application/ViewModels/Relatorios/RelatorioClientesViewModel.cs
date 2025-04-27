namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class RelatorioClientesViewModel
    {
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        public int TotalClientes { get; set; }
        public int ClientesAtivos { get; set; }
        public int ClientesNovos { get; set; }
        public double MediaAtendimentosPorCliente { get; set; }

        public List<ClienteDetalhadoViewModel> TopClientes { get; set; } = new List<ClienteDetalhadoViewModel>();
    }

    public class ClienteDetalhadoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeAtendimentos { get; set; }
        public decimal ValorTotalGasto { get; set; }
        public DateTime UltimoAtendimento { get; set; }
    }

    public class EstatisticasClientesViewModel
    {
        public int QtdEstacionamentos { get; set; }
        public int QtdMensalistas { get; set; }
    }
}
