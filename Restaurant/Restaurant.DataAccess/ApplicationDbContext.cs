using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Restaurant.DataAccess.Configurations;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new OrdersConfiguration());
            modelBuilder.Configurations.Add(new MenuItemsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}