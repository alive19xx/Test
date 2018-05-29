using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Orders = new GenericRepository<Order>(_context);
            MenuItems = new GenericRepository<MenuItem>(_context);
            Users = new GenericRepository<User>(_context);
            OrderItems = new GenericRepository<OrderItem>(_context);
        }
        
        public IRepository<Order> Orders { get; }
        public IRepository<MenuItem> MenuItems { get; }
        public IRepository<User> Users { get; }
        public IRepository<OrderItem> OrderItems { get; }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
