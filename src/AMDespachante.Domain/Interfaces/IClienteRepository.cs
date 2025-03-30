using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Models;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Domain.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
    Task<IEnumerable<Cliente>> GetAll();
    Task<Cliente?> GetById(Guid Id);
    Task<Cliente> GetByIdWithVeiculos(Guid id);
    void Add(Cliente recurso);
    void Update(Cliente recurso);
    void Delete(Cliente recurso);
}
