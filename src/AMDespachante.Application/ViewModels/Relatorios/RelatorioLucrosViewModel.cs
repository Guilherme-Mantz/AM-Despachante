using AMDespachante.Domain.Enums;

namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class RelatorioLucrosViewModel
    {
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal LucroTotal => TotalEntradas - TotalSaidas;
        public decimal PercentualLucro => TotalEntradas > 0 ? LucroTotal / TotalEntradas * 100 : 0;

        public List<LucroPorPeriodoViewModel> LucrosPorPeriodo { get; set; } = new List<LucroPorPeriodoViewModel>();
        public List<LucroPorTipoServicoViewModel> LucrosPorTipoServico { get; set; } = new List<LucroPorTipoServicoViewModel>();
        public List<LucroPorFormaPagamentoViewModel> LucrosPorFormaPagamento { get; set; } = new List<LucroPorFormaPagamentoViewModel>();
    }

    public class LucroPorPeriodoViewModel
    {
        public string Periodo { get; set; }
        public decimal ValorEntradas { get; set; }
        public decimal ValorSaidas { get; set; }
        public decimal Lucro => ValorEntradas - ValorSaidas;
    }

    public class LucroPorTipoServicoViewModel
    {
        public string TipoServico { get; set; }
        public string DescricaoServico { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorEntradas { get; set; }
        public decimal ValorSaidas { get; set; }
        public decimal Lucro => ValorEntradas - ValorSaidas;
    }

    public class LucroPorFormaPagamentoViewModel
    {
        public FormaPagamento FormaPagamento { get; set; }
        public string DescricaoFormaPagamento { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Percentual { get; set; }
    }
}
