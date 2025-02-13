using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Models;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Domain.Interfaces;
public interface IRecursoRepository : IRepository<Recurso>
{
    Task<IEnumerable<Recurso>> GetAll();
    Task<Recurso?> GetById(Guid Id);

    void Add(Recurso recurso);
    void Update(Recurso recurso);
    void Delete(Recurso recurso);
    Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
}
