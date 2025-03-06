using AMDespachante.Application.ViewModels;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Interfaces
{
    public interface IRecursoAppService : IDisposable
    {
        Task<PagedResult<RecursoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<RecursoViewModel>> GetAll();
        Task<RecursoViewModel?> GetById(Guid Id);
        Task<RecursoViewModel?> GetByCpf(string cpf);
        Task<bool> IsFirstAccess(string cpf);

        Task<ValidationResult> Add(RecursoViewModel recurso);
        Task<ValidationResult> Update(RecursoViewModel recurso);
        Task<ValidationResult> Delete(Guid Id);

        Task<ValidationResult> DeactiveFirtsAccess(string cpf);
    }
}
