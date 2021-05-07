using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.DTO
{
    class OrderDetails
    {
        public UserDetails UserDetails { get; set; }
        public System.DateTime DateCreated { get; set; }
        public List<OrderItemDetails> OrderItems { get; set; }
        public double TotalSum { get; set; }
    }
}
