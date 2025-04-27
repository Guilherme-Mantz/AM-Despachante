namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class RelatorioVeiculosViewModel
    {
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        public int TotalVeiculos { get; set; }
        public int VeiculosEmAtendimento { get; set; }

        public List<VeiculoPorMarcaViewModel> VeiculosPorMarca { get; set; } = new List<VeiculoPorMarcaViewModel>();
        public List<VeiculoPorAnoViewModel> VeiculosPorAno { get; set; } = new List<VeiculoPorAnoViewModel>();
        public List<VeiculoDetalhadoViewModel> VeiculosMaisAtendidos { get; set; } = new List<VeiculoDetalhadoViewModel>();
    }

    public class VeiculoPorMarcaViewModel
    {
        public string Marca { get; set; }
        public int Quantidade { get; set; }
        public decimal Percentual { get; set; }
    }

    public class VeiculoPorAnoViewModel
    {
        public int Ano { get; set; }
        public int Quantidade { get; set; }
    }

    public class VeiculoDetalhadoViewModel
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public int QuantidadeAtendimentos { get; set; }
        public decimal ValorTotalServicos { get; set; }
    }
}
