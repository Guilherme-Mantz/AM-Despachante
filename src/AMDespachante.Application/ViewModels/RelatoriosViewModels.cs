using AMDespachante.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDespachante.Application.ViewModels
{
    // ViewModels/Relatorios/FiltroRelatorioViewModel.cs
    public class FiltroRelatorioViewModel
    {
        public DateTime DataInicio { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFim { get; set; } = DateTime.Now;
    }

    // ViewModels/Relatorios/RelatorioLucrosViewModel.cs
    public class RelatorioLucrosViewModel
    {
        // Propriedades para filtros
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        // Propriedades para exibição de dados
        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal LucroTotal => TotalEntradas - TotalSaidas;
        public decimal PercentualLucro => TotalEntradas > 0 ? (LucroTotal / TotalEntradas) * 100 : 0;

        // Dados para gráficos e tabelas
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

    // ViewModels/Relatorios/RelatorioAtendimentosViewModel.cs
    public class RelatorioAtendimentosViewModel
    {
        // Propriedades para filtros
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        // Propriedades para exibição de dados
        public int TotalAtendimentos { get; set; }
        public int AtendimentosFinalizados { get; set; }
        public int AtendimentosEmAndamento { get; set; }
        public int AtendimentosPagos { get; set; }
        public int AtendimentosPendentes { get; set; }
        public decimal ValorMedioAtendimento { get; set; }

        // Dados para gráficos e tabelas
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

    // ViewModels/Relatorios/RelatorioClientesViewModel.cs
    public class RelatorioClientesViewModel
    {
        // Propriedades para filtros
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        // Propriedades para exibição de dados
        public int TotalClientes { get; set; }
        public int ClientesAtivos { get; set; }
        public int ClientesNovos { get; set; }
        public decimal MediaAtendimentosPorCliente { get; set; }

        // Dados para gráficos e tabelas
        public List<ClientePorRegiaoViewModel> ClientesPorRegiao { get; set; } = new List<ClientePorRegiaoViewModel>();
        public List<ClienteDetalhadoViewModel> TopClientes { get; set; } = new List<ClienteDetalhadoViewModel>();
    }

    public class ClientePorRegiaoViewModel
    {
        public string Regiao { get; set; }
        public int Quantidade { get; set; }
        public decimal Percentual { get; set; }
    }

    public class ClienteDetalhadoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeAtendimentos { get; set; }
        public decimal ValorTotalGasto { get; set; }
        public DateTime UltimoAtendimento { get; set; }
    }

    // ViewModels/Relatorios/RelatorioVeiculosViewModel.cs
    public class RelatorioVeiculosViewModel
    {
        // Propriedades para filtros
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        // Propriedades para exibição de dados
        public int TotalVeiculos { get; set; }
        public int VeiculosEmAtendimento { get; set; }

        // Dados para gráficos e tabelas
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

    // ViewModels/Relatorios/RelatorioServicosViewModel.cs
    public class RelatorioServicosViewModel
    {
        // Propriedades para filtros
        public FiltroRelatorioViewModel Filtro { get; set; } = new FiltroRelatorioViewModel();

        // Propriedades para exibição de dados
        public int TotalServicos { get; set; }
        public decimal ValorTotalServicos { get; set; }

        // Dados para gráficos e tabelas
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
