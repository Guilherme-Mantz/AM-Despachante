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
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly AmDespachanteContext _db;
        private readonly DbSet<Atendimento> _dbSet;

        public AtendimentoRepository(AmDespachanteContext db)
        {
            _db = db;
            _dbSet = _db.Set<Atendimento>();
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder = "asc", string searchTerm = null, string sortField = "data")
        {
            page = Math.Max(0, page);
            pageSize = Math.Clamp(pageSize, 1, 100);
            sortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "asc" : (sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc");
            sortField = string.IsNullOrWhiteSpace(sortField) ? "data" : sortField.ToLower();

            var query = _db.Atendimentos.AsNoTracking().Include(c => c.Cliente).Include(v => v.Veiculo).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var sanitizedTerm = searchTerm.Replace("%", "\\%").Replace("_", "\\_");

                var matchingStatus = Enum.GetValues<StatusAtendimentoEnum>()
                    .Where(c => c.GetEnumDisplayName()
                        .Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var matchingServicos = Enum.GetValues<TipoServicoEnum>()
                    .Where(c => c.GetEnumDisplayName()
                        .Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                bool isDecimal = decimal.TryParse(searchTerm, out decimal decimalValue);
                bool isDate = DateTime.TryParse(searchTerm, out DateTime dateValue);

                query = query.Where(r =>
                    (isDate && r.Data.Date == dateValue.Date) ||

                    EF.Functions.Like(r.Cliente.Nome ?? string.Empty, $"%{sanitizedTerm}%") ||
                    EF.Functions.Like(r.Veiculo.Placa ?? string.Empty, $"%{sanitizedTerm}%") ||

                    (matchingServicos.Count != 0 && matchingServicos.Contains(r.Servico)) ||
                    (matchingStatus.Count != 0 && matchingStatus.Contains(r.Status)) ||

                    (isDecimal && (r.ValorEntrada == decimalValue || r.ValorSaida == decimalValue))
                );
            }

            var sortExpressions = new Dictionary<string, Expression<Func<Atendimento, object>>>
            {
                ["data"] = x => x.Data,
                ["cliente"] = x => x.Cliente.Nome ?? string.Empty,
                ["servico"] = x => x.Servico,
                ["placa"] = x => x.Veiculo.Placa ?? string.Empty,
                ["valorEntrada"] = x => x.ValorEntrada,
                ["valorSaida"] = x => x.ValorSaida,
                ["status"] = x => x.Status
            };

            var sortExpression = sortExpressions.TryGetValue(sortField, out Expression<Func<Atendimento, object>> value)
                ? value : sortExpressions["data"];

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

        public async Task<IEnumerable<Atendimento>> GetAll()
        {
            return await _dbSet.AsNoTracking().Include(c => c.Cliente).Include(v => v.Veiculo).ToListAsync();
        }

        public async Task<IEnumerable<Atendimento>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet.AsNoTracking()
                .Where(a => a.Data >= dataInicio && a.Data <= dataFim)
                .ToListAsync();
        }

        public async Task<Atendimento> GetById(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }
        public async Task<Atendimento> GetByIdWithIncludes(Guid Id)
        {
            return await _dbSet.AsNoTracking()
                .Include(c => c.Cliente)
                .Include(v => v.Veiculo)
                .FirstOrDefaultAsync(a => a.Id == Id);
        }

        public void Add(Atendimento atendimento)
        {
            _dbSet.Add(atendimento);
        }

        public void Update(Atendimento atendimento)
        {
            _dbSet.Update(atendimento);
        }

        public void Delete(Atendimento atendimento)
        {
            _dbSet.Remove(atendimento);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
