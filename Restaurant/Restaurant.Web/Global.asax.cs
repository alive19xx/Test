using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Restaurant.DataAccess;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Services.Identity;
using Restaurant.Web.AutomapperProfiles;

namespace Restaurant.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UpdateDatabase();
            AddAdmin();
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutomapperWebProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        private void UpdateDatabase()
        {
            var migrator = new DbMigrator(new DataAccess.Migrations.Configuration());
            migrator.Update();
        }

        private async Task AddAdmin()
        {
            var userStore = new ApplicationUserStore();
            var userManager = new ApplicationUserManager(userStore);
            if (userManager.Users.Any(x => x.UserName == "Admin" && x.Position == UserPosition.Admin))
            {
                var admin = new User()
                {
                    UserName = "Admin",
                    FirstName = "Admin",
                    SecondName = "Admin",
                    Position = UserPosition.Admin
                };
                await userManager.CreateAsync(admin, "TotalAdmin");
            }
        }
    }
}
    
