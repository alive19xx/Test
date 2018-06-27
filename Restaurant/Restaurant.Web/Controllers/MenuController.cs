using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;
using Restaurant.Services;
using Restaurant.Web.Attributes;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Controllers
{
    [Authorize]
    [ClaimsAuthorize(ClaimTypes.Role, new []{"Admin" })]
    public class MenuController : Controller
    {
        #region Controller setup
        private readonly IMenuItemsService _menuService;
        public MenuController(IMenuItemsService menuService)
        {
            _menuService = menuService;
        }
        #endregion

        #region Public Actions
        public ActionResult Index()
        {
            var menuItems = _menuService.Get();
            var menuItemsViewModel = Mapper.Map<IEnumerable<MenuItem>, IEnumerable<MenuItemViewModel>>(menuItems);
            return View(menuItemsViewModel);
        }

        public ActionResult Details(int id)
        {
            var menuItem = _menuService.Get(id);

            if (menuItem == null)
                return HttpNotFound();
            
            var menuItemViewModel = Mapper.Map<MenuItem, MenuItemViewModel>(menuItem);
            return View(menuItemViewModel);
        }

        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(MenuItemViewModel model)
        {
            if (!ModelState.IsValid || (model.Id != null && model.Id.Value != 0))
                return View("Create", model);
            var menuItem = Mapper.Map<MenuItemViewModel, MenuItem>(model);

            return Save(menuItem);
        }

        public ActionResult Edit(int id)
        {
            var menuItem = _menuService.Get(id);
            if (menuItem == null)
                return HttpNotFound();

            var menuItemViewModel = Mapper.Map<MenuItem, MenuItemViewModel>(menuItem);
            return View(menuItemViewModel);
        }

        [HttpPost]
        public ActionResult Edit(MenuItemViewModel model)
        {
            if (!ModelState.IsValid || (model.Id == null || model.Id.Value == 0))
                return View("Edit", model);
            var menuItem = Mapper.Map<MenuItemViewModel, MenuItem>(model);

            return Save(menuItem);
        }

        public ActionResult Delete(int id)
        {
            _menuService.DeleteMenuItem(id);
            return RedirectToAction("Index");
        }
        #endregion
        
        #region Private Actions
        private ActionResult Save(MenuItem item)
        {
            if (item.Id==0)
                _menuService.AddMenuItem(item);
            else
            {
                _menuService.UpdateMenuItem(item);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}