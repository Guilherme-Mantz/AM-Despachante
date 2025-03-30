using AMDespachante.Application.ViewModels;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<PagedResult<ClienteViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<ClienteViewModel>> GetAll();
        Task<ClienteViewModel?> GetById(Guid Id);
        Task<ClienteViewModel> GetByIdWithVeiculos(Guid Id);

        Task<ValidationResult> Add(ClienteViewModel cliente);
        Task<ValidationResult> Update(ClienteViewModel cliente);
        Task<ValidationResult> Delete(Guid Id);
    }
}
