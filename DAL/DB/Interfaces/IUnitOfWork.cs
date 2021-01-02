using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save(CancellationToken cancelationToken = default);

        IDbContextTransaction CreateTransaction();
    }
}
