using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels.Relatorios;
using AMDespachante.Domain.Enums;
using AMDespachante.Domain.Extensions;
using AMDespachante.Domain.Interfaces;
using System.Globalization;

namespace AMDespachante.Application.Services
{
    public class RelatorioAppService(
        IAtendimentoRepository atendimentoRepository,
        IClienteRepository clienteRepository, 
        IVeiculoRepository veiculoRepository) 
        : IRelatorioAppService
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

                LucrosPorPeriodo = [.. atendimentos
                .GroupBy(a => new { Mes = a.Data.Month, Ano = a.Data.Year })
                .Select(g => new LucroPorPeriodoViewModel
                {
                    Periodo = $"{g.Key.Mes}/{g.Key.Ano}",
                    ValorEntradas = g.Sum(a => a.ValorEntrada),
                    ValorSaidas = g.Sum(a => a.ValorSaida)
                })
                .OrderBy(l => l.Periodo)],

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

            viewModel.AtendimentosPorTipoServico = [.. atendimentos
                .GroupBy(a => a.Servico)
                .Select(g => new AtendimentoPorTipoServicoViewModel
                {
                    TipoServico = g.Key,
                    DescricaoServico = g.FirstOrDefault().Servico.GetEnumDisplayName(),
                    Quantidade = g.Count(),
                    ValorTotal = g.Sum(a => a.ValorEntrada)
                })
                .OrderByDescending(a => a.Quantidade)];

            viewModel.AtendimentosPorPeriodo = [.. atendimentos
                .GroupBy(a => new { Mes = a.Data.Month, Ano = a.Data.Year })
                .Select(g => new AtendimentoPorPeriodoViewModel
                {
                    Periodo = $"{g.Key.Mes}/{g.Key.Ano}",
                    Quantidade = g.Count(),
                    ValorEntradas = g.Sum(a => a.ValorEntrada)
                })
                .OrderBy(a => a.Periodo)];

            viewModel.UltimosAtendimentos = atendimentos
                .OrderByDescending(a => a.Data)
                .Take(10)
                .Select(a => new AtendimentoDetalhadoViewModel
                {
                    Id = a.Id,
                    Data = a.Data,
                    Cliente = a.Cliente?.Nome ?? "N/A",
                    Veiculo = a.Veiculo != null ? $"{a.Veiculo.Modelo} ({a.Veiculo.Placa})" : "N/A",
                    TipoServico = a.Servico.GetEnumDisplayName(),
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
            var clientes = await _clienteRepository.ObterTodosComAtendimentosAsync();

            foreach (var cliente in clientes)
            {
                cliente.Atendimentos = cliente.Atendimentos
                    .Where(a => a.Data >= filtro.DataInicio && a.Data <= filtro.DataFim)
                    .ToList();
            }

            var clientesAtivos = clientes.Where(c => c.Atendimentos.Count != 0).ToList();

            var clientesNovos = clientes.Where(c => c.Criado >= filtro.DataInicio && c.Criado <= filtro.DataFim).ToList();

            var viewModel = new RelatorioClientesViewModel
            {
                Filtro = filtro,
                TotalClientes = clientes.Count(),
                ClientesAtivos = clientesAtivos.Count,
                ClientesNovos = clientesNovos.Count,
                MediaAtendimentosPorCliente = clientesAtivos.Count != 0
                    ? clientesAtivos.Average(c => c.Atendimentos.Count)
                    : 0,
                TopClientes = clientesAtivos
                .OrderByDescending(c => c.Atendimentos.Sum(a => a.ValorEntrada))
                .Take(10)
                .Select(c => new ClienteDetalhadoViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    DataCadastro = c.Criado,
                    QuantidadeAtendimentos = c.Atendimentos.Count,
                    ValorTotalGasto = c.Atendimentos.Sum(a => a.ValorEntrada),
                    UltimoAtendimento = c.Atendimentos.Count != 0
                        ? c.Atendimentos.Max(a => a.Data)
                        : DateTime.MinValue
                })
                .ToList()
            };

            return viewModel;
        }

        public async Task<RelatorioVeiculosViewModel> RelatorioVeiculos(FiltroRelatorioViewModel filtro)
        {
            var veiculos = await _veiculoRepository.ObterTodosComAtendimentosAsync();

            foreach (var veiculo in veiculos)
            {
                veiculo.Atendimentos = veiculo.Atendimentos
                    .Where(a => a.Data >= filtro.DataInicio && a.Data <= filtro.DataFim)
                    .ToList();
            }

            var veiculosEmAtendimento = veiculos.Where(v => v.Atendimentos.Count != 0).ToList();

            var viewModel = new RelatorioVeiculosViewModel
            {
                Filtro = filtro,
                TotalVeiculos = veiculos.Count(),
                VeiculosEmAtendimento = veiculosEmAtendimento.Count,
                VeiculosPorMarca = [.. veiculos
                .GroupBy(v => ExtrairMarca(v.Modelo))
                .Select(g => new VeiculoPorMarcaViewModel
                {
                    Marca = g.Key,
                    Quantidade = g.Count(),
                    Percentual = veiculos.Any() ? (decimal)g.Count() / veiculos.Count() * 100 : 0
                })
                .OrderByDescending(v => v.Quantidade)],

                VeiculosPorAno = [.. veiculos
                .GroupBy(v => int.Parse(v.AnoFabricacao))
                .Select(g => new VeiculoPorAnoViewModel
                {
                    Ano = g.Key,
                    Quantidade = g.Count()
                })
                .OrderByDescending(v => v.Ano)],

                VeiculosMaisAtendidos = veiculosEmAtendimento
                .OrderByDescending(v => v.Atendimentos.Count)
                .Take(10)
                .Select(v => new VeiculoDetalhadoViewModel
                {
                    Id = v.Id,
                    Placa = v.Placa,
                    Modelo = v.Modelo,
                    Marca = ExtrairMarca(v.Modelo),
                    Ano = int.Parse(v.AnoFabricacao),
                    QuantidadeAtendimentos = v.Atendimentos.Count,
                    ValorTotalServicos = v.Atendimentos.Sum(a => a.ValorEntrada)
                })
                .ToList()
            };

            return viewModel;
        }

        public async Task<EstatisticasClientesViewModel> ObterEstatisticasClientes(FiltroRelatorioViewModel filtro)
        {
            var clientes = await _clienteRepository.GetAll();

            return new EstatisticasClientesViewModel
            {
                QtdEstacionamentos = clientes.Count(c => c.EhEstacionamento),
                QtdMensalistas = clientes.Count(c => c.PagaMensalidade)
            };
        }

        public async Task<RelatorioServicosViewModel> RelatorioServicos(FiltroRelatorioViewModel filtro)
        {
            var atendimentos = await _atendimentoRepository.ObterPorPeriodoAsync(filtro.DataInicio, filtro.DataFim);

            var totalServicos = atendimentos.Count();
            var valorTotalServicos = atendimentos.Sum(a => a.ValorEntrada);

            var servicosPorTipo = atendimentos
                .GroupBy(a => a.Servico)
                .Select(grupo => new ServicoPorTipoViewModel
                {
                    Tipo = grupo.Key.GetEnumDisplayName(),
                    Quantidade = grupo.Count(),
                    ValorTotal = grupo.Sum(a => a.ValorEntrada),
                    Percentual = totalServicos > 0 ? (decimal)grupo.Count() / totalServicos * 100 : 0
                })
                .OrderByDescending(s => s.Quantidade)
                .ToList();

            var servicosPorPeriodo = new List<ServicoPorPeriodoViewModel>();

            var diasNoPeriodo = (filtro.DataFim - filtro.DataInicio).TotalDays;

            if (diasNoPeriodo <= 60)
            {
                servicosPorPeriodo = [.. atendimentos
                    .GroupBy(a => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        a.Data, CalendarWeekRule.FirstDay, DayOfWeek.Sunday))
                    .Select(grupo => new ServicoPorPeriodoViewModel
                    {
                        Periodo = $"Semana {grupo.Key}",
                        Quantidade = grupo.Count(),
                        ValorTotal = grupo.Sum(a => a.ValorEntrada)
                    })
                    .OrderBy(s => s.Periodo)];
            }
            else
            {
                servicosPorPeriodo = [.. atendimentos
                    .GroupBy(a => new { a.Data.Year, a.Data.Month })
                    .Select(grupo => new ServicoPorPeriodoViewModel
                    {
                        Periodo = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(grupo.Key.Month)}/{grupo.Key.Year}",
                        Quantidade = grupo.Count(),
                        ValorTotal = grupo.Sum(a => a.ValorEntrada)
                    })
                    .OrderBy(s => s.Periodo)];
            }

            var servicosMaisExecutados = atendimentos
                .GroupBy(a => new { a.Servico, Valor = a.ValorEntrada })
                .Select(grupo => new ServicoMaisExecutadoViewModel
                {
                    Descricao = grupo.Key.Servico.GetEnumDisplayName(),
                    Quantidade = grupo.Count(),
                    ValorUnitario = grupo.Key.Valor,
                    ValorTotal = grupo.Count() * grupo.Key.Valor
                })
                .OrderByDescending(s => s.Quantidade)
                .Take(10)
                .ToList();

            var viewModel = new RelatorioServicosViewModel
            {
                Filtro = filtro,
                TotalServicos = totalServicos,
                ValorTotalServicos = valorTotalServicos,
                ServicosPorTipo = servicosPorTipo,
                ServicosPorPeriodo = servicosPorPeriodo,
                ServicosMaisExecutados = servicosMaisExecutados
            };

            return viewModel;
        }

        private string ExtrairMarca(string modelo)
        {
            if (string.IsNullOrEmpty(modelo))
                return "Não informado";

            string marca = modelo.Split(' ').FirstOrDefault() ?? "Não informado";
            return marca;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
