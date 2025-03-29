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

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var query = _db.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r =>
                    EF.Functions.Like(r.Nome, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Email, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Cpf, $"%{searchTerm}%")
                );
            }

            query = sortOrder == "asc"
                ? query.OrderBy(GetSortProperty(sortField))
                : query.OrderByDescending(GetSortProperty(sortField));

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

        public void Dispose() => _db.Dispose();

        private Expression<Func<Cliente, object>> GetSortProperty(string sortField)
        {
            return sortField?.ToLower() switch
            {
                "nome" => x => x.Nome,
                "email" => x => x.Email,
                "cpf" => x => x.Cpf,
                _ => x => x.Nome
            };
        }
    }
}
