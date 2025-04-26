using AMDespachante.Application.ViewModels;

namespace AMDespachante.Application.Interfaces
{
    public interface IRelatorioAppService : IDisposable
    {
        Task<RelatorioLucrosViewModel> RelatorioLucros(FiltroRelatorioViewModel filtro);
        Task<RelatorioAtendimentosViewModel> RelatorioAtendimentos(FiltroRelatorioViewModel filtro);
        Task<RelatorioClientesViewModel> RelatorioClientes(FiltroRelatorioViewModel filtro);
    }
}
