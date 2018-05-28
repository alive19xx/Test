using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Enums;

namespace Restaurant.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddOrderItems(int id, IEnumerable<int> itemsIds)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            var newItems = _unitOfWork.MenuItems.GetBy(o => itemsIds.Contains(o.Id)).ToList();

            foreach (var item in newItems)
            {
                orderInDb.OrderItems.Add(item);
            }
            _unitOfWork.Complete();
        }

        public void OrderAccepted(Order order)
        {
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();
        }

        public void OrderReady(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Ready;
            _unitOfWork.Complete();
        }

        public void OrderServed(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Served;
            _unitOfWork.Complete();
        }

        public void OrderComplete(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Closed;
        }

        public IEnumerable<Order> Get()
        {
            var ordersInDb = _unitOfWork.Orders.Get();
            return ordersInDb;
        }
        public IEnumerable<Order> Get(OrderStatus status)
        {
            var ordersInDb = GetBy(o => o.OrderStatus == status);
            return ordersInDb;
        }

        public Order Get(int id)
        {
            var ordersInDb = _unitOfWork.Orders.GetSingleOrDefault(o=>o.Id == id);
            return ordersInDb;
        }
        public IEnumerable<Order> GetBy(Func<Order, bool> predicate)
        {
            var ordersInDb = _unitOfWork.Orders.GetBy(predicate);
            return ordersInDb;
        }

    }
}
