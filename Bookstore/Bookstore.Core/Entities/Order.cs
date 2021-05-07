using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public System.DateTime DateCreated { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; }

        // public List<OrdersBooks> OrdersBooks { get; set; }
        // public double Sum { get; set; }
    }
}
