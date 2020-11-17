using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Interfaces
{
    public interface IOrderRepository
    {
        void MakeOrder(Book book);
    }
}
