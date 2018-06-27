using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Services;
using Restaurant.Web.Attributes;
using Restaurant.Web.ViewModels;
using Restaurant.Web.ViewModels.OrderForm;
using OrderFormViewModel = Restaurant.Web.ViewModels.OrderForm.OrderFormViewModel;

namespace Restaurant.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        #region Controller Setup
        private readonly IOrdersService _ordersService;
        private readonly IMenuItemsService _menuItemsService;
        public OrdersController(IOrdersService orderService, IMenuItemsService menuItemsService)
        {
            _ordersService = orderService;
            _menuItemsService = menuItemsService;
        }
        #endregion

        public ActionResult Index()
        {
            //var viewModel =Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_ordersService
            //        .GetByStatus(orderStatusFilter));

            var viewModel = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_ordersService
                .Get());

            return View(viewModel);
        }

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Create()
        {
            var menuItemsViewModel = Mapper
                .Map<IEnumerable<MenuItem>, IEnumerable<MenuItemViewModel>>(_menuItemsService.Get());
            ViewBag.Menu = menuItemsViewModel;
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
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

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Edit(int id)
        {
            ViewBag.Menu = Mapper
                .Map<IEnumerable<MenuItem>, IEnumerable<MenuItemViewModel>>(_menuItemsService.Get());

            var order = _ordersService.Get(id);
            if (order == null)
                return HttpNotFound();

            if (order.OrderStatus == OrderStatus.Closed)
                return View("InvalidStatus");

            var orderFormViewModel = Mapper.Map<Order, OrderFormViewModel>(_ordersService.Get(id));
            return View(orderFormViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Edit(OrderFormViewModel model)
        {
            var order = Mapper.Map<OrderFormViewModel, Order>(model);
            var result = _ordersService.UpdateOrder(order);

            if (result == ChangeOrderStatusResult.InvalidStatus)
                return View("InvalidStatus");

            if (result == ChangeOrderStatusResult.NotFound)
                return HttpNotFound();

            return RedirectToAction("Details", new { id = order.Id });
        }

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Cook" })]
        public ActionResult Ready(int id)
        {
            var result = _ordersService.ReadyOrder(id);

            if (result == ChangeOrderStatusResult.InvalidStatus)
                return View("InvalidStatus");

            if (result == ChangeOrderStatusResult.NotFound)
                return HttpNotFound();

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Serve(int id)
        {
            var result = _ordersService.ServeOrder(id);

            if (result == ChangeOrderStatusResult.InvalidStatus)
                return View("InvalidStatus");

            if (result == ChangeOrderStatusResult.NotFound)
                return HttpNotFound();

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Close(int id)
        {
            var result = _ordersService.CompleteOrder(id);

            if (result == ChangeOrderStatusResult.InvalidStatus)
                return View("InvalidStatus");

            if (result == ChangeOrderStatusResult.NotFound)
                return HttpNotFound();

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(ClaimTypes.Role, new[] { "Admin", "Waiter" })]
        public ActionResult Cancel(int id)
        {
            var result = _ordersService.CancelOrder(id);

            if (result == ChangeOrderStatusResult.InvalidStatus)
                return View("InvalidStatus");

            if (result == ChangeOrderStatusResult.NotFound)
                return HttpNotFound();
            
            return RedirectToAction("Index");
        }

    }
}