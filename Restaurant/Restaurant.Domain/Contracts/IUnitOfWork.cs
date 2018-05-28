using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Order> Orders { get; }
        IRepository<MenuItem> MenuItems { get; }
        IRepository<User> Users { get; }

        void Complete();
    }
}
