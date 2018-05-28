using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Contracts;

namespace Restaurant.DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T:class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetBy(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public T GetSingleOrDefault(Func<T, bool> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
