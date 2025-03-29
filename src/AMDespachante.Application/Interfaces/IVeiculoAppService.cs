using AMDespachante.Application.ViewModels;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Interfaces
{
    public interface IVeiculoAppService : IDisposable
    {
        Task<PagedResult<VeiculoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<VeiculoViewModel>> GetAll();
        Task<VeiculoViewModel?> GetById(Guid id);

        Task<ValidationResult> Add(VeiculoViewModel veiculo);
        Task<ValidationResult> Update(VeiculoViewModel veiculo);
        Task<ValidationResult> Delete(Guid id);
    }
}
