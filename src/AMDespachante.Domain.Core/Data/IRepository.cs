using AMDespachante.Domain.Core.DomainObjects;

namespace AMDespachante.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
