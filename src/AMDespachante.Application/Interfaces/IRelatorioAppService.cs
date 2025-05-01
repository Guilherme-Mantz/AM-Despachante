using AMDespachante.Application.ViewModels.Relatorios;

namespace AMDespachante.Application.Interfaces
{
    public interface IRelatorioAppService : IDisposable
    {
        Task<RelatorioLucrosViewModel> RelatorioLucros(FiltroRelatorioViewModel filtro);
        Task<RelatorioAtendimentosViewModel> RelatorioAtendimentos(FiltroRelatorioViewModel filtro);
        Task<RelatorioClientesViewModel> RelatorioClientes(FiltroRelatorioViewModel filtro);
        Task<RelatorioVeiculosViewModel> RelatorioVeiculos(FiltroRelatorioViewModel filtro);
        Task<RelatorioServicosViewModel> RelatorioServicos(FiltroRelatorioViewModel filtro);
        Task<EstatisticasClientesViewModel> ObterEstatisticasClientes(FiltroRelatorioViewModel filtro);
    }
}
