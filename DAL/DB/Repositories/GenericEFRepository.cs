using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace BookArchive
{
    public class GenericEFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext dbx;

        public GenericEFRepository(DbContext dbContext)
        {
            this.dbx = dbContext;
        }

        public IEnumerable<T> Get()
        {
            return dbx.Set<T>();
        }

        public virtual bool Contains(Expression<Func<T, bool>> predicate)
        {
            return dbx.Set<T>().AsNoTracking().Count(predicate) > 0;
        }

        public virtual void Add(T entity, IDbTransaction transaction = default)
        {
            dbx.Set<T>().Add(entity);
        }

        public virtual void Update(T entity, IDbTransaction transaction = default)
        {
            var entry = dbx.Entry(entity);
            dbx.Set<T>().Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity, IDbTransaction transaction = default)
        {
            dbx.Set<T>().Remove(entity);
        }

        public void CreateSavePoint(IDbTransaction transaction)
        {
            //transaction is not used but it forces the caller to be in the scope of a transaction
            dbx.SaveChanges();
            dbx.ChangeTracker.Entries().ToList()
                .ForEach(x => x.State = EntityState.Detached);
        }

    }
}