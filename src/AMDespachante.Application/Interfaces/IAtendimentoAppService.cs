using AMDespachante.Application.ViewModels;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Interfaces
{
    public interface IAtendimentoAppService : IDisposable
    {
        Task<PagedResult<AtendimentoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<AtendimentoViewModel>> GetAll();
        Task<AtendimentoViewModel?> GetById(Guid Id);
        Task<AtendimentoViewModel> GetByIdWithIncludes(Guid Id);

        Task<ValidationResult> Add(AtendimentoViewModel atendimento);
        Task<ValidationResult> Update(AtendimentoViewModel atendimento);
        Task<ValidationResult> Delete(Guid Id);
    }
}
