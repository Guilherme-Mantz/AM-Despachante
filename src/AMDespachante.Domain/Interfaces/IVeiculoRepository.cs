using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Models;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Domain.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<Veiculo>> GetAll();
        Task<IEnumerable<Veiculo>> ObterTodosComAtendimentosPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<Veiculo?> GetById(Guid Id);
        Task<IEnumerable<Veiculo>> GetByClienteIdAsync(Guid id);
        Task<bool> PlacaExists(Guid id, string placa);
        void Add(Veiculo veiculo);
        void Update(Veiculo veiculo);
        void Delete(Veiculo veiculo);
    }
}
