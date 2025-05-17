namespace AMDespachante.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit(bool isAutomatedJob = false);
        void Reset();
    }
}
