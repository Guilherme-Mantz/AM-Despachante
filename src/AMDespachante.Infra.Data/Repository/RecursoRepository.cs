using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using AMDespachante.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Recurso>> GetAll() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<Recurso> GetById(Guid id) => await _dbSet.FindAsync(id);

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
    }
}
