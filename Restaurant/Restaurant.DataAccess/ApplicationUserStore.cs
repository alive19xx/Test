using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Restaurant.Domain.Entities;

namespace Restaurant.DataAccess
{
    public class ApplicationUserStore:UserStore<User>
    {
        public ApplicationUserStore() : this(new ApplicationDbContext())
        {
        }

        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
        }
    }
    
}
