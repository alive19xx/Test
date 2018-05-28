using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Contracts;
using Restaurant.Domain.Entities;

namespace Restaurant.Services
{
    public class MenuItemsService : IMenuItemsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddMenuItem(MenuItem newMenuItem)
        {
            _unitOfWork.MenuItems.Add(newMenuItem);
            _unitOfWork.Complete();
        }

        public IEnumerable<MenuItem> Get()
        {
            return _unitOfWork.MenuItems.Get();
        }

        public MenuItem Get(int id)
        {
            return _unitOfWork.MenuItems.GetSingleOrDefault(m => m.Id == id);
        }

        public void UpdateMenuItem(MenuItem newMenuItem)
        {
            var itemInDb = _unitOfWork.MenuItems.GetSingleOrDefault(m => m.Id == newMenuItem.Id);
            itemInDb.Name = newMenuItem.Name;
            itemInDb.Price = newMenuItem.Price;
            _unitOfWork.Complete();
        }

        public void DeleteMenuItem(int id)
        {
            var itemInDb = _unitOfWork.MenuItems.GetSingleOrDefault(m => m.Id == id);
            _unitOfWork.MenuItems.Remove(itemInDb);
            _unitOfWork.Complete();
        }
    }
}
