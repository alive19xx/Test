using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Contracts
{
    public interface IOrdersService
    {
        void OrderAccepted(Order order);
        void OrderReady(int id);
        void OrderServed(int id);
        void OrderComplete(int id);

        void AddOrderItems(int id, IEnumerable<int> itemsIds);
        
        IEnumerable<Order> Get();
        IEnumerable<Order> Get(OrderStatus status);

        Order Get(int id);

        IEnumerable<Order> GetBy(Func<Order,bool> predicate);
    }
}
