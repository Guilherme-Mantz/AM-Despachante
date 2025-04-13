using AMDespachante.Domain.Core.Data;
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

        public async Task<PagedResult> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var query = _db.Atendimentos.AsNoTracking().Include(c => c.Cliente).Include(v => v.Veiculo).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r =>
                    EF.Functions.Like(r.Cliente.Nome, $"%{searchTerm}%") ||
                    EF.Functions.Like(r.Veiculo.Placa, $"%{searchTerm}%")
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

        public async Task<IEnumerable<Atendimento>> GetAll()
        {
            return await _dbSet.AsNoTracking().Include(c => c.Cliente).Include(v => v.Veiculo).ToListAsync();
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

        private Expression<Func<Atendimento, object>> GetSortProperty(string sortField)
        {
            return sortField?.ToLower() switch
            {
                "clienteNome" => x => x.Cliente.Nome,
                "placa" => x => x.Veiculo.Placa,
                _ => x => x.Cliente.Nome
            };
        }
    }
}
