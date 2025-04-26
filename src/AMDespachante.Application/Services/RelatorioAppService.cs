using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Enums;
using AMDespachante.Domain.Extensions;
using AMDespachante.Domain.Interfaces;

namespace AMDespachante.Application.Services
{
    public class RelatorioAppService(IAtendimentoRepository atendimentoRepository, IClienteRepository clienteRepository, IVeiculoRepository veiculoRepository) : IRelatorioAppService
    {
        private readonly IAtendimentoRepository _atendimentoRepository = atendimentoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IVeiculoRepository _veiculoRepository = veiculoRepository;

        public async Task<RelatorioLucrosViewModel> RelatorioLucros(FiltroRelatorioViewModel filtro)
        {
            var atendimentos = await _atendimentoRepository.ObterPorPeriodoAsync(filtro.DataInicio, filtro.DataFim);

            var viewModel = new RelatorioLucrosViewModel
            {
                Filtro = filtro,
                TotalEntradas = atendimentos.Sum(a => a.ValorEntrada),
                TotalSaidas = atendimentos.Sum(a => a.ValorSaida),

                // Agrupar por período (mês/ano)
                LucrosPorPeriodo = [.. atendimentos
                .GroupBy(a => new { Mes = a.Data.Month, Ano = a.Data.Year })
                .Select(g => new LucroPorPeriodoViewModel
                {
                    Periodo = $"{g.Key.Mes}/{g.Key.Ano}",
                    ValorEntradas = g.Sum(a => a.ValorEntrada),
                    ValorSaidas = g.Sum(a => a.ValorSaida)
                })
                .OrderBy(l => l.Periodo)],

                // Agrupar por tipo de serviço
                LucrosPorTipoServico = [.. atendimentos
                .GroupBy(a => a.Servico)
                .Select(g => new LucroPorTipoServicoViewModel
                {
                    TipoServico = g.Key.ToString(),
                    DescricaoServico = g.FirstOrDefault().Servico.GetEnumDisplayName(),
                    Quantidade = g.Count(),
                    ValorEntradas = g.Sum(a => a.ValorEntrada),
                    ValorSaidas = g.Sum(a => a.ValorSaida)
                })
                .OrderByDescending(l => l.Lucro)]
            };

            // Agrupar por forma de pagamento
            decimal totalValor = atendimentos.Sum(a => a.ValorEntrada);
            viewModel.LucrosPorFormaPagamento = [.. atendimentos
                .GroupBy(a => a.FormaPagamento)
                .Select(g => new LucroPorFormaPagamentoViewModel
                {
                    FormaPagamento = g.Key,
                    DescricaoFormaPagamento = g.Key.ToString(),
                    Quantidade = g.Count(),
                    ValorTotal = g.Sum(a => a.ValorEntrada),
                    Percentual = totalValor > 0 ? (g.Sum(a => a.ValorEntrada) / totalValor) * 100 : 0
                })
                .OrderByDescending(l => l.ValorTotal)];

            return viewModel;
        }

        public async Task<RelatorioAtendimentosViewModel> RelatorioAtendimentos(FiltroRelatorioViewModel filtro)
        {
            var atendimentos = await _atendimentoRepository.ObterPorPeriodoAsync(filtro.DataInicio, filtro.DataFim);

            var viewModel = new RelatorioAtendimentosViewModel
            {
                Filtro = filtro,
                TotalAtendimentos = atendimentos.Count(),
                AtendimentosFinalizados = atendimentos.Count(a => a.Status == StatusAtendimentoEnum.Concluido),
                AtendimentosPagos = atendimentos.Count(a => a.Status == StatusAtendimentoEnum.Pago),
                AtendimentosPendentes = atendimentos.Count(a => a.Status == StatusAtendimentoEnum.Pendente),
                ValorMedioAtendimento = atendimentos.Any() ? atendimentos.Average(a => a.ValorEntrada) : 0
            };

            // Agrupar por status
            int totalAtendimentos = atendimentos.Count();
            viewModel.AtendimentosPorStatus = [.. atendimentos
                .GroupBy(a => a.Status)
                .Select(g => new AtendimentoPorStatusViewModel
                {
                    Status = g.Key,
                    DescricaoStatus = g.Key.ToString(),
                    Quantidade = g.Count(),
                    Percentual = totalAtendimentos > 0 ? (decimal)g.Count() / totalAtendimentos * 100 : 0
                })
                .OrderByDescending(a => a.Quantidade)];

            // Agrupar por tipo de serviço
            viewModel.AtendimentosPorTipoServico = [.. atendimentos
                .GroupBy(a => a.Servico)
                .Select(g => new AtendimentoPorTipoServicoViewModel
                {
                    TipoServico = g.Key,
                    DescricaoServico = g.Key.ToString(),
                    Quantidade = g.Count(),
                    ValorTotal = g.Sum(a => a.ValorEntrada)
                })
                .OrderByDescending(a => a.Quantidade)];

            // Agrupar por período (mês/ano)
            viewModel.AtendimentosPorPeriodo = [.. atendimentos
                .GroupBy(a => new { Mes = a.Data.Month, Ano = a.Data.Year })
                .Select(g => new AtendimentoPorPeriodoViewModel
                {
                    Periodo = $"{g.Key.Mes}/{g.Key.Ano}",
                    Quantidade = g.Count(),
                    ValorEntradas = g.Sum(a => a.ValorEntrada)
                })
                .OrderBy(a => a.Periodo)];

            // Últimos atendimentos
            viewModel.UltimosAtendimentos = atendimentos
                .OrderByDescending(a => a.Data)
                .Take(10)
                .Select(a => new AtendimentoDetalhadoViewModel
                {
                    Id = a.Id,
                    Data = a.Data,
                    Cliente = a.Cliente?.Nome ?? "N/A",
                    Veiculo = a.Veiculo != null ? $"{a.Veiculo.Modelo} ({a.Veiculo.Placa})" : "N/A",
                    TipoServico = a.Servico.ToString(),
                    Status = a.Status,
                    DescricaoStatus = a.Status.ToString(),
                    ValorEntrada = a.ValorEntrada,
                    ValorSaida = a.ValorSaida,
                    FormaPagamento = a.FormaPagamento.ToString(),
                    EstaPago = a.EstaPago,
                    NumeroATPV = a.NumeroATPV
                })
                .ToList();

            return viewModel;
        }

        public async Task<RelatorioClientesViewModel> RelatorioClientes(FiltroRelatorioViewModel filtro)
        {
            return new RelatorioClientesViewModel();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
