using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess.Configurations
{
    public class OrderItemConfguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfguration()
        {
            HasKey(i => new {i.OrderId, i.MenuItemId});
            HasRequired(i => i.MenuItem).WithMany().HasForeignKey(i => i.MenuItemId);
        }
    }
}
