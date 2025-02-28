using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Enums;
using AMDespachante.Domain.Extensions;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using AMDespachante.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AMDespachante.Infra.Data.Repository
{
    public class RecursoRepository : IRecursoRepository
    {
        private readonly AmDespachanteContext _db;
        private readonly DbSet<Recurso> _dbSet;

        public RecursoRepository(AmDespachanteContext context)
        {
            _db = context;
            _dbSet = _db.Set<Recurso>();
        }

        public IUnitOfWork UnitOfWork => _db;


        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var query = _db.Recursos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var cargoEnum = Enum.GetValues<CargoEnum>()
                    .FirstOrDefault(c => c.GetEnumDisplayName()
                    .Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

                query = query.Where(r =>
                    EF.Functions.Like(r.Nome, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Email, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Cpf, $"%{searchTerm}%") ||
                    (r.Cargo == cargoEnum)
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

        public async Task<IEnumerable<Recurso>> GetAll() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<Recurso> GetById(Guid id) => await _dbSet.FindAsync(id);

        public async Task<(bool email, bool cpf)> EmailOrCpfExists(string email, string cpf)
        {
            var recurso = await _dbSet
                .Where(r => r.Email == email || r.Cpf == cpf)
                .Select(r => new { r.Email, r.Cpf })
                .FirstOrDefaultAsync();

            if (recurso == null) return (email: false, cpf: false);

            return (
                email: recurso.Email == email,
                cpf: recurso.Cpf == cpf
            );
        }

        public void Add(Recurso recurso)
        {
            _dbSet.Add(recurso);
        }

        public void Update(Recurso recurso)
        {
            _dbSet.Update(recurso);
        }

        public void Delete(Recurso recurso)
        {
            _dbSet.Remove(recurso);
        }

        public void Dispose() => _db.Dispose();

        private Expression<Func<Recurso, object>> GetSortProperty(string sortField)
        {
            return sortField?.ToLower() switch
            {
                "nome" => x => x.Nome,
                "email" => x => x.Email,
                "cpf" => x => x.Cpf,
                "cargo" => x => x.Cargo,
                "ativo" => x => x.Ativo,
                _ => x => x.Nome
            };
        }
    }
}
