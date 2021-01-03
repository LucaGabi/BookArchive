using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace BookArchive
{
    public class EFDbTransaction : IDbTransaction, IDisposable
    {
        private readonly IDbContextTransaction transaction;

        public EFDbTransaction(DbContext dbContext)
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            transaction.Dispose();
        }
        
    }
}
