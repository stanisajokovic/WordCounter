using Microsoft.EntityFrameworkCore.Storage;

namespace WordCounterAPI.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbContextTransaction Begin();
    }
}