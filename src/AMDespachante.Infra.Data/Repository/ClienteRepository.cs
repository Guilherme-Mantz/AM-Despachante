using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using AMDespachante.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AMDespachante.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AmDespachanteContext _db;
        private readonly DbSet<Cliente> _dbSet;

        public ClienteRepository(AmDespachanteContext db)
        {
            _db = db;
            _dbSet = _db.Set<Cliente>();
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder = "asc", string searchTerm = null, string sortField = "nome")
        {
            page = Math.Max(0, page);
            pageSize = Math.Clamp(pageSize, 1, 100);
            sortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "asc" : (sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc");
            sortField = string.IsNullOrWhiteSpace(sortField) ? "nome" : sortField.ToLower();

            var query = _db.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var sanitizedTerm = searchTerm.Replace("%", "\\%").Replace("_", "\\_");

                query = query.Where(r =>
                    EF.Functions.Like(r.Nome ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Email ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.DocumentoFiscal ?? string.Empty, $"%{sanitizedTerm}%")
                );
            }

            var sortExpressions = new Dictionary<string, Expression<Func<Cliente, object>>>
            {
                ["nome"] = x => x.Nome ?? string.Empty,
                ["email"] = x => x.Email ?? string.Empty,
                ["documentoFiscal"] = x => x.DocumentoFiscal ?? string.Empty
            };

            var sortExpression = sortExpressions.TryGetValue(sortField, out Expression<Func<Cliente, object>> value)
                ? value : sortExpressions["nome"];

            query = sortOrder == "desc"
                ? query.OrderByDescending(sortExpression)
                : query.OrderBy(sortExpression);

            var totalCount = await query.CountAsync();
            var data = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult
            {
                Queryable = data.AsQueryable(),
                PageCount = totalCount
            };
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Cliente> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Cliente> GetByIdWithVeiculos(Guid id)
        {
            return await _dbSet.Include(x => x.Veiculos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Cliente recurso)
        {
            _dbSet.Add(recurso);
        }

        public void Update(Cliente recurso)
        {
            _dbSet.Update(recurso);
        }

        public void Delete(Cliente recurso)
        {
            _dbSet.Remove(recurso);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<IEnumerable<Cliente>> ObterTodosComAtendimentosAsync()
        {
            return await _dbSet.Include(x => x.Atendimentos).ToListAsync();
        }
    }
}
