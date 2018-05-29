using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurant.Domain.Contracts
{
    public interface IRepository<T> where T:class
    {
        T GetSingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetBy(Expression<Func<T,bool>> predicate, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Get(params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Remove(T entity);
    }
}
