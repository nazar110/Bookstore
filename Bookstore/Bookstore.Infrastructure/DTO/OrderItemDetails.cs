using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.DTO
{
    class OrderItemDetails
    {
        public int Quantity { get; set; }
        public BookAllDetails Book { get; set; }
    }
}
