using Microsoft.AspNet.Identity;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Restaurant.DataAccess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Restaurant.DataAccess.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new ApplicationUserStore(context);
            var userManager = new UserManager<User>(userStore);
            if (!userManager.Users.Any(x => x.UserName == "Admin"))
            {
                var admin = new User()
                {
                    UserName = "Admin",
                    FirstName = "Admin",
                    SecondName = "Restaurant",
                    Position = UserPosition.Admin
                };
                userManager.Create(admin, "RestaurantAdmin");
            }
        }
    }
}
