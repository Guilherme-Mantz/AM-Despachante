namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class RelatorioServicosViewModel
    {
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        public int TotalServicos { get; set; }
        public decimal ValorTotalServicos { get; set; }

        public List<ServicoPorTipoViewModel> ServicosPorTipo { get; set; } = new List<ServicoPorTipoViewModel>();
        public List<ServicoPorPeriodoViewModel> ServicosPorPeriodo { get; set; } = new List<ServicoPorPeriodoViewModel>();
        public List<ServicoMaisExecutadoViewModel> ServicosMaisExecutados { get; set; } = new List<ServicoMaisExecutadoViewModel>();
    }

    public class ServicoPorTipoViewModel
    {
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Percentual { get; set; }
    }

    public class ServicoPorPeriodoViewModel
    {
        public string Periodo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class ServicoMaisExecutadoViewModel
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
