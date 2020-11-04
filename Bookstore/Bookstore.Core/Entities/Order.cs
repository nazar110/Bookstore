using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Book> Books { get; set; }
    }
}
