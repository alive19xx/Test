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
        void AcceptOrder(Order order, IEnumerable<OrderItem> orderItems);
        void ReadyOrder(int id);
        void ServeOrder(int id);
        void CompleteOrder(int id);
        void CancelOrder(int id);

        void UpdateOrder(Order order);

        IEnumerable<Order> Get();
        Order Get(int id);
        IEnumerable<Order> GetBy(Expression<Func<Order,bool>> predicate);
    }
}
