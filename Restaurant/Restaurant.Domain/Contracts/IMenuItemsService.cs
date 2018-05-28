using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Contracts
{
    public interface IMenuItemsService
    {
        void AddMenuItem(MenuItem newMenuItem);
        IEnumerable<MenuItem> Get();
        MenuItem Get(int id);
        void UpdateMenuItem(MenuItem newMenuItem);
        void DeleteMenuItem(int id);
    }
}
