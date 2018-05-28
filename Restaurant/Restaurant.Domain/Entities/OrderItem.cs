using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    public class OrderItem
    {
        private int _numberOfItems;

        public MenuItem MenuItem { get; set; }

        public int NumberOfItems
        {
            get
            {
                if (_numberOfItems<0)
                    return 0;
                return _numberOfItems;
            }
            set
            {
                if (_numberOfItems < 0)
                    _numberOfItems = 0;
                _numberOfItems = value;
            }
        }
    }
}
