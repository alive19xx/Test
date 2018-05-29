using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Enums;

namespace Restaurant.Services
{
    public class OrdersService : IOrdersService
    {
        #region Service setup
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods

        public Order Get(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(
                o => o.Id == id,
                o => o.OrderItems.Select(i => i.MenuItem));
            return orderInDb;
        }

        public void AcceptOrder(Order order)
        {
            order.DateTimeCreated = DateTime.Now;
            order.OrderStatus = OrderStatus.Accepted;
            order.Id = 0;

            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();
        }

        public void AcceptOrder(Order order, IEnumerable<OrderItem> orderItems)
        {

        }

        public void ReadyOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Ready;
            _unitOfWork.Complete();
        }

        public void ServeOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Served;
            _unitOfWork.Complete();
        }

        public void CompleteOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            orderInDb.OrderStatus = OrderStatus.Closed;
        }

        public void UpdateOrder(Order order)
        {

        }

        public void CancelOrder(int id)
        {
            var orderInDb =
                _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            _unitOfWork.Orders.Remove(orderInDb);
            _unitOfWork.Complete();
        }

        public IEnumerable<Order> Get()
        {
            var ordersInDb = _unitOfWork.Orders.Get();
            return ordersInDb;
        }

        public IEnumerable<Order> GetBy(Expression<Func<Order, bool>> predicate)
        {
            var ordersInDb = _unitOfWork.Orders.GetBy(predicate);
            return ordersInDb;
        }

        public void AddOrderItems(int orderId, IEnumerable<OrderItem> newItems)
        {
            var orderItemsInDb = _unitOfWork.OrderItems.GetBy(i => i.OrderId == orderId, i => i.MenuItem) ?? new List<OrderItem>();
            foreach (var newItem in newItems)
            {
                var orderItemInDb = orderItemsInDb.SingleOrDefault(i => i.MenuItemId == newItem.MenuItem.Id);
                if (orderItemInDb != null)
                {
                    orderItemInDb.NumberOfItems = newItem.NumberOfItems;
                }
                else
                {
                    var newOrderItem = new OrderItem()
                    {
                        MenuItemId = newItem.MenuItem.Id,
                        OrderId = orderId,
                        NumberOfItems = newItem.NumberOfItems
                    };
                    _unitOfWork.OrderItems.Add(newOrderItem);
                }
            }
            _unitOfWork.Complete();
        }
        #endregion
    }
}
