using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Domain.Entities;
using Restaurant.Web.Controllers;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.AutomapperProfiles
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<MenuItem, MenuItemViewModel>();
            CreateMap<MenuItemViewModel, MenuItem>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<User,UserAdminViewModel>();
        }
        
    }
}