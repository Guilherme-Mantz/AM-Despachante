using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using AMDespachante.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AMDespachante.Infra.Data.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AmDespachanteContext _db;
        private readonly DbSet<Veiculo> _dbSet;

        public VeiculoRepository(AmDespachanteContext db)
        {
            _db = db;
            _dbSet = _db.Set<Veiculo>();
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var query = _db.Veiculos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {

                query = query.Where(r =>
                    EF.Functions.Like(r.Placa, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Renavam, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Modelo, $"%{searchTerm}%")
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

        public async Task<IEnumerable<Veiculo>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Veiculo> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Veiculo>> GetByClienteIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.ClienteId == id).ToListAsync();
        }

        public void Add(Veiculo veiculo)
        {
            _dbSet.Add(veiculo);
        }

        public void Update(Veiculo veiculo)
        {
            _dbSet.Update(veiculo);
        }

        public void Delete(Veiculo veiculo)
        {
            _dbSet.Remove(veiculo);
        }

        public void Dispose() => _db.Dispose();

        private Expression<Func<Veiculo, object>> GetSortProperty(string sortField)
        {
            return sortField?.ToLower() switch
            {
                "placa" => x => x.Placa,
                "renavam" => x => x.Renavam,
                "modelo" => x => x.Modelo,
                _ => x => x.Modelo
            };
        }
    }
}
