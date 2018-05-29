using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Domain.Entities;
using Restaurant.Web.Controllers;
using Restaurant.Web.ViewModels;
using Restaurant.Web.ViewModels.OrderForm;
using OrderFormViewModel = Restaurant.Web.ViewModels.OrderForm.OrderFormViewModel;

namespace Restaurant.Web.AutomapperProfiles
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<OrderViewModel, Order>();
            CreateMap<OrderItemViewModel, OrderItem>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderItemViewModel>();
            
            CreateMap<MenuItem, MenuItemViewModel>();
            CreateMap<MenuItemViewModel, MenuItem>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<User,UserAdminViewModel>();

            //Order form
            CreateMap<OrderFormViewModel, Order>();
            CreateMap<OrderFormItemViewModel, OrderItem>();
        }
        
    }
}