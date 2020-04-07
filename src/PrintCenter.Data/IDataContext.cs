using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrintCenter.Data
{
    public interface IDataContext : ITransaction
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<T> DbSet<T>() where T : class;

        IQueryable<T> Query<T>() where T : class;
    }
}