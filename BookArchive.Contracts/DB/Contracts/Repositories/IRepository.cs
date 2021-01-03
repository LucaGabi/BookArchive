using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookArchive
{
    public interface IRepository<T>
    {
        public IEnumerable<T> Get();


        //public T Find(Expression<Func<T, bool>> predicate);


        public bool Contains(Expression<Func<T, bool>> predicate);


        public void Add(T entity, IDbTransaction transaction = default);


        public void Update(T entity, IDbTransaction transaction = default);


        public void Delete(T entity, IDbTransaction transaction = default);
    }
}