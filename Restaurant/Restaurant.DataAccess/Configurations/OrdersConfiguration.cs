using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess.Configurations
{
    class OrdersConfiguration : EntityTypeConfiguration<Order>
    {
        public OrdersConfiguration()
        {
            HasKey(o => o.Id);
            HasMany(o => o.OrderItems).WithRequired().HasForeignKey(i => i.OrderId);
        }
    }
}
