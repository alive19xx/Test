using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Contracts
{
    public interface IRepository<T> where T:class
    {
        T GetSingleOrDefault(Func<T, bool> predicate);
        IEnumerable<T> GetBy(Func<T,bool> predicateFunc);
        IEnumerable<T> Get();
        void Add(T entity);
        void Remove(T entity);
    }
}
