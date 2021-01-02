using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.DAL
{
    public partial class BookArchiveUnitOfWork : IUnitOfWork, IBookArchiveUOW
    {
        private readonly BookArchiveDataContext dataContext;
        private readonly IServiceProvider serviceProvider;


        public BookArchiveUnitOfWork(BookArchiveDataContext dataContext, IServiceProvider serviceProvider)
        {
            this.dataContext = dataContext;
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            dataContext.Dispose();
        }


        public IDbContextTransaction CreateTransaction()
        {
            return dataContext.Database.BeginTransaction();
        }

        public async Task<int> Save(CancellationToken cancelationToken)
        {
            return await dataContext.Save(cancelationToken);
        }
    }
}