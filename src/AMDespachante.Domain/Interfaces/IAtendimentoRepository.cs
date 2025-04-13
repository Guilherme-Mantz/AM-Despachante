using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Models;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Domain.Interfaces
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null);
        Task<IEnumerable<Atendimento>> GetAll();
        Task<Atendimento?> GetById(Guid Id);
        Task<Atendimento?> GetByIdWithIncludes(Guid Id);

        void Add(Atendimento atendimento);
        void Update(Atendimento atendimento);
        void Delete(Atendimento atendimento);
    }
}
