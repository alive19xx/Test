using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess.Configurations
{
    class MenuItemsConfiguration : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemsConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Name).IsRequired().HasMaxLength(255);
        }
    }
}
