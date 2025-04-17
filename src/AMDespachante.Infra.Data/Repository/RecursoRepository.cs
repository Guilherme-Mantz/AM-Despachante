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

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder = "asc", string searchTerm = null, string sortField = "nome")
        {
            page = Math.Max(0, page);
            pageSize = Math.Clamp(pageSize, 1, 100);
            sortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "asc" : (sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc");
            sortField = string.IsNullOrWhiteSpace(sortField) ? "nome" : sortField.ToLower();

            var query = _db.Recursos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var sanitizedTerm = searchTerm.Replace("%", "\\%").Replace("_", "\\_");

                var matchingCargos = Enum.GetValues<CargoEnum>()
                    .Where(c => c.GetEnumDisplayName()
                        .Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                query = query.Where(r =>
                    EF.Functions.Like(r.Nome ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Email ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Cpf ?? string.Empty, $"%{sanitizedTerm}%") ||
                    (matchingCargos.Count != 0 && matchingCargos.Contains(r.Cargo))
                );
            }

            var sortExpressions = new Dictionary<string, Expression<Func<Recurso, object>>>
            {
                ["nome"] = x => x.Nome ?? string.Empty,
                ["email"] = x => x.Email ?? string.Empty,
                ["cpf"] = x => x.Cpf ?? string.Empty,
                ["cargo"] = x => x.Cargo.GetEnumDisplayName(),
                ["ativo"] = x => x.Ativo
            };

            var sortExpression = sortExpressions.TryGetValue(sortField, out Expression<Func<Recurso, object>> value)
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

        public async Task<IEnumerable<Recurso>> GetAll() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<Recurso> GetById(Guid id) => await _dbSet.FindAsync(id);

        public async Task<Recurso> GetByCpf(string cpf) => await _dbSet.FirstOrDefaultAsync(x => x.Cpf == cpf);

        public async Task<bool> IsFirstAccess(string cpf)
        {
            return await _dbSet
                .Where(x => x.Cpf == cpf)
                .Select(x => x.PrimeiroAcesso)
                .FirstOrDefaultAsync();
        }

        public async Task<(bool email, bool cpf)> EmailOrCpfExists(string email, string cpf)
        {
            var existingRecursos = await _dbSet
                .Where(r =>
                    (!string.IsNullOrEmpty(email) && r.Email == email) ||
                    (!string.IsNullOrEmpty(cpf) && r.Cpf == cpf))
                .Select(r => new { r.Email, r.Cpf })
                .ToListAsync();

            return (
                email: existingRecursos.Any(r => r.Email == email),
                cpf: existingRecursos.Any(r => r.Cpf == cpf)
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

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
