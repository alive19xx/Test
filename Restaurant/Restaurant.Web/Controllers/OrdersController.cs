using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Services;
using Restaurant.Web.Dtos;
using Restaurant.Web.ViewModels;
using Restaurant.Web.ViewModels.OrderForm;
using OrderFormViewModel = Restaurant.Web.ViewModels.OrderForm.OrderFormViewModel;

namespace Restaurant.Web.Controllers
{
    public class OrdersController : Controller
    {
        #region Controller Setup
        private readonly IOrdersService _ordersService;
        private readonly IMenuItemsService _menuItemsService;
        public OrdersController(IOrdersService orderService,IMenuItemsService menuItemsService)
        {
            _ordersService = orderService;
            _menuItemsService = menuItemsService;
        }
        #endregion

        public ActionResult Index()
        {
            var orders = _ordersService.Get();
            var ordersViewModel = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            return View(ordersViewModel);
        }

        public ActionResult Create()
        {
            var menuItemsViewModel = Mapper
                .Map<IEnumerable<MenuItem>, IEnumerable<MenuItemViewModel>>(_menuItemsService.Get());
            ViewBag.Menu = menuItemsViewModel;
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderFormViewModel model)
        {
            var newOrder = Mapper.Map<OrderFormViewModel, Order>(model);
            _ordersService.AcceptOrder(newOrder);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var orderViewModel = Mapper.Map<Order, OrderViewModel>(_ordersService.Get(id));
            return View(orderViewModel);
        }

        public ActionResult Ready(int id)
        {
            _ordersService.ReadyOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Serve(int id)
        {
            _ordersService.ServeOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Complete(int id)
        {
            _ordersService.CompleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return HttpNotFound();
        }

        public ActionResult Cancel(int id)
        {
            _ordersService.CancelOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeOrderItems(int id, IEnumerable<OrderItemDto> dto)
        {
            return View();
        }
    }
}