using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class OrderDTO : IOrder
    {
        public void MakeOrder(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
