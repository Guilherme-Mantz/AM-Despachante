using AMDespachante.Application.ViewModels;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Interfaces
{
    public interface IRecursoAppService
    {
        Task<PagedResult<RecursoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<RecursoViewModel>> GetAll();
        Task<RecursoViewModel?> GetById(Guid Id);

        void Add(RecursoViewModel recurso);
        void Update(RecursoViewModel recurso);
        void Delete(Guid Id);
    }
}
