using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookArchive.DAL
{
    public interface IRepository<T>
    {
        public IEnumerable<T> Get();


        //public T Find(Expression<Func<T, bool>> predicate);


        public bool Contains(Expression<Func<T, bool>> predicate);


        public void Add(T entity, IDbContextTransaction transaction = default);


        public void Update(T entity, IDbContextTransaction transaction = default);


        public void Delete(T entity, IDbContextTransaction transaction = default);
    }
}