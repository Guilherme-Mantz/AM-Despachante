using AMDespachante.Domain.Enums;

namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class RelatorioAtendimentosViewModel
    {
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        public int TotalAtendimentos { get; set; }
        public int AtendimentosFinalizados { get; set; }
        public int AtendimentosEmAndamento { get; set; }
        public int AtendimentosPagos { get; set; }
        public int AtendimentosPendentes { get; set; }
        public decimal ValorMedioAtendimento { get; set; }

        public List<AtendimentoPorStatusViewModel> AtendimentosPorStatus { get; set; } = new List<AtendimentoPorStatusViewModel>();
        public List<AtendimentoPorTipoServicoViewModel> AtendimentosPorTipoServico { get; set; } = new List<AtendimentoPorTipoServicoViewModel>();
        public List<AtendimentoPorPeriodoViewModel> AtendimentosPorPeriodo { get; set; } = new List<AtendimentoPorPeriodoViewModel>();
        public List<AtendimentoDetalhadoViewModel> UltimosAtendimentos { get; set; } = new List<AtendimentoDetalhadoViewModel>();
    }

    public class AtendimentoPorStatusViewModel
    {
        public StatusAtendimentoEnum Status { get; set; }
        public string DescricaoStatus { get; set; }
        public int Quantidade { get; set; }
        public decimal Percentual { get; set; }
    }

    public class AtendimentoPorTipoServicoViewModel
    {
        public TipoServicoEnum TipoServico { get; set; }
        public string DescricaoServico { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class AtendimentoPorPeriodoViewModel
    {
        public string Periodo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorEntradas { get; set; }
    }

    public class AtendimentoDetalhadoViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
        public string Veiculo { get; set; }
        public string TipoServico { get; set; }
        public StatusAtendimentoEnum Status { get; set; }
        public string DescricaoStatus { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorSaida { get; set; }
        public string FormaPagamento { get; set; }
        public bool EstaPago { get; set; }
        public string NumeroATPV { get; set; }
    }
}
