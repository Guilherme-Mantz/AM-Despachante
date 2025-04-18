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

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder = "asc", string searchTerm = null, string sortField = "clienteNome")
        {
            page = Math.Max(0, page);
            pageSize = Math.Clamp(pageSize, 1, 100);
            sortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "asc" : (sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc");
            sortField = string.IsNullOrWhiteSpace(sortField) ? "clienteNome" : sortField.ToLower();

            var query = _db.Veiculos.Include(c => c.Cliente).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var sanitizedTerm = searchTerm.Replace("%", "\\%").Replace("_", "\\_");
                var matchingTipos = Enum.GetValues<TipoVeiculoEnum>()
                    .Where(c => c.GetEnumDisplayName()
                        .Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                query = query.Where(r =>
                    EF.Functions.Like(r.Cliente.Nome ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Placa ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Renavam ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Modelo ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.AnoFabricacao ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.AnoModelo ?? string.Empty, $"%{sanitizedTerm}%") ||
                    (matchingTipos.Count != 0 && matchingTipos.Contains(r.TipoVeiculo))
                );
            }

            var sortExpressions = new Dictionary<string, Expression<Func<Veiculo, object>>>
            {
                ["clienteNome"] = x => x.Cliente.Nome ?? string.Empty,
                ["placa"] = x => x.Placa ?? string.Empty,
                ["renavam"] = x => x.Renavam ?? string.Empty,
                ["modelo"] = x => x.Modelo ?? string.Empty,
                ["anoFabricacao"] = x => x.AnoFabricacao ?? string.Empty,
                ["anoModelo"] = x => x.AnoModelo ?? string.Empty,
                ["tipoVeiculo"] = x => x.TipoVeiculo
            };

            var sortExpression = sortExpressions.TryGetValue(sortField, out Expression<Func<Veiculo, object>> value)
                    ? value : sortExpressions["clienteNome"];

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

        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<bool> PlacaExists(Guid id, string placa)
        {
            return await _dbSet.AnyAsync(x => x.Id != id && x.Placa == placa);
        }
    }
}
