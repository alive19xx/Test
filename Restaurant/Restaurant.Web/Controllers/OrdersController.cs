using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;
using Restaurant.Services;
using Restaurant.Web.Dtos;

namespace Restaurant.Web.Controllers
{
    public class OrdersController : Controller
    {
        #region Controller Setup
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService orderService)
        {
            _ordersService = orderService;
        }
        #endregion

        public ActionResult Index()
        {
            var orders = _ordersService.Get();
            return View(orders);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult New(OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var newOrder = Mapper.Map<OrderViewModel, Order>(model);
            _ordersService.OrderAccepted(newOrder);
            return View("Index");
        }

        public ActionResult Ready(int id)
        {
            _ordersService.OrderReady(id);
            return RedirectToAction("Index");
        }

        public ActionResult Serve(int id)
        {
            _ordersService.OrderServed(id);
            return RedirectToAction("Index");
        }

        public ActionResult Complete(int id)
        {
            _ordersService.OrderComplete(id);
            return RedirectToAction("Index");
        }

        public ActionResult AddItems(OrderItemsDto dto)
        {
            return View();
        }
    }
}