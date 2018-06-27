using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Contracts
{
    public interface IOrdersService
    {
        void AcceptOrder(Order order);
        ChangeOrderStatusResult ReadyOrder(int id);
        ChangeOrderStatusResult ServeOrder(int id);
        ChangeOrderStatusResult CompleteOrder(int id);
        ChangeOrderStatusResult CancelOrder(int id);
        ChangeOrderStatusResult UpdateOrder(Order order);

        IEnumerable<Order> Get();
        Order Get(int id);
        IEnumerable<Order> GetByStatus(IEnumerable<OrderStatus> statusFilter);
        
        IEnumerable<Order> GetBy(Expression<Func<Order,bool>> predicate);
    }
}
