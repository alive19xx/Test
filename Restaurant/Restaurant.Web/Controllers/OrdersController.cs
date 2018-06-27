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

        public ActionResult Edit(int id)
        {
            ViewBag.Menu = Mapper
                .Map<IEnumerable<MenuItem>, IEnumerable<MenuItemViewModel>>(_menuItemsService.Get());
            var orderFormViewModel = Mapper.Map<Order, OrderFormViewModel>(_ordersService.Get(id));
            return View(orderFormViewModel);
        }

        [HttpPost]
        public ActionResult Edit(OrderFormViewModel model)
        {
            var order = Mapper.Map<OrderFormViewModel, Order>(model);
            _ordersService.UpdateOrder(order);
            return RedirectToAction("Details", new { id = order.Id });
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

        public ActionResult Close(int id)
        {
            _ordersService.CompleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            _ordersService.CancelOrder(id);
            return RedirectToAction("Index");
        }

        

        //#region Private helpers
        //private IEnumerable<OrderStatus> GetStatusFilter()
        //{
        //    IList<OrderStatus> statusFilter = new List<OrderStatus>();
        //    var user = User as ClaimsPrincipal;
        //    if (user?.Claims == null)
        //        return statusFilter;

        //    foreach (var claim in user.Claims)
        //    {
        //        if (claim.Type != ClaimTypes.Role)
        //            continue;
        //        var newFilterItems = new List<OrderStatus>();
        //        switch (claim.Value)
        //        {
        //            case "Admin":
        //                newFilterItems.Add(OrderStatus.Accepted);
        //                newFilterItems.Add(OrderStatus.Closed);
        //                newFilterItems.Add(OrderStatus.Ready);
        //                newFilterItems.Add(OrderStatus.Served);
        //                break;
        //            case "Waiter":
        //                newFilterItems.Add(OrderStatus.Accepted);
        //                newFilterItems.Add(OrderStatus.Ready);
        //                newFilterItems.Add(OrderStatus.Served);
        //                break;
        //            case "Cook":
        //                newFilterItems.Add(OrderStatus.Accepted);
        //                break;
        //            default:
        //                break;
        //        }
        //        statusFilter = statusFilter.Union(newFilterItems) as IList<OrderStatus>;
        //    }

        //    return statusFilter;
        //}

        //#endregion
    }
}