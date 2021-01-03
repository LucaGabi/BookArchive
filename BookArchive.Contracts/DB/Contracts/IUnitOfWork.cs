using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save(CancellationToken cancelationToken = default);

        IDbTransaction CreateTransaction();
    }
}
