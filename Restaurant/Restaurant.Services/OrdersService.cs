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


            if (order.OrderItems != null && order.OrderItems.Any())
                order.OrderItems= normalizedOrderItemsList(order.OrderItems);

            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();
        }

        public void AcceptOrder(Order order, IEnumerable<OrderItem> orderItems)
        {

        }

        public void ReadyOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            if (orderInDb == null || orderInDb.OrderStatus != OrderStatus.Accepted)
                return;

            orderInDb.OrderStatus = OrderStatus.Ready;
            _unitOfWork.Complete();
        }

        public void ServeOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            if (orderInDb == null || orderInDb.OrderStatus != OrderStatus.Ready)
                return;

            orderInDb.OrderStatus = OrderStatus.Served;
            _unitOfWork.Complete();
        }

        public void CompleteOrder(int id)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == id);
            if (orderInDb == null || orderInDb.OrderStatus != OrderStatus.Served)
                return;

            orderInDb.OrderStatus = OrderStatus.Closed;
            _unitOfWork.Complete();
        }

        public void UpdateOrder(Order order)
        {
            var orderInDb = _unitOfWork.Orders.GetSingleOrDefault(o => o.Id == order.Id);
            orderInDb.TableNumber = order.TableNumber;
            updateOrderItems(order.Id, order.OrderItems);
            _unitOfWork.Complete();
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

        public IEnumerable<Order> GetByStatus(IEnumerable<OrderStatus> statusFilter)
        {
            if (statusFilter == null || !statusFilter.Any())
                return null;

            var ordersInDb = _unitOfWork.Orders.GetBy(o=> statusFilter.Any(f=>f==o.OrderStatus));
            return ordersInDb;
        }

        #endregion

        #region private
        private static IList<OrderItem> normalizedOrderItemsList(IList<OrderItem> orderItems)
        {
            var noramlizedOrderItems = new List<OrderItem>();

            foreach (var item in orderItems)
            {
                if (item.NumberOfItems == 0) continue;
                if (noramlizedOrderItems.All(i => i.MenuItemId != item.MenuItemId))
                    noramlizedOrderItems.Add(new OrderItem()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = item.OrderId,
                        NumberOfItems = 0
                    });
                noramlizedOrderItems.Single(i => i.MenuItemId == item.MenuItemId).NumberOfItems += item.NumberOfItems;
            }

            return noramlizedOrderItems;
        }

        private void updateOrderItems(int orderId, IList<OrderItem> newOrderItems)
        {
            var orderItemsInDb = _unitOfWork.OrderItems.GetBy(i => i.OrderId == orderId);
            newOrderItems = normalizedOrderItemsList(newOrderItems);

            foreach (var item in orderItemsInDb)
            {
                if (newOrderItems.Any(i => i.MenuItemId == item.MenuItemId)) continue;

                _unitOfWork.OrderItems.Remove(item);
            }

            foreach (var item in newOrderItems)
            {
                var itemInDb = orderItemsInDb.SingleOrDefault(i => i.MenuItemId == item.MenuItemId);
                if (itemInDb != null)
                {
                    itemInDb.NumberOfItems = item.NumberOfItems;
                }
                else
                {
                    itemInDb = new OrderItem()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = orderId,
                        NumberOfItems = item.NumberOfItems
                    };
                    _unitOfWork.OrderItems.Add(itemInDb);
                }
            }
        }

        #endregion
    }


}
