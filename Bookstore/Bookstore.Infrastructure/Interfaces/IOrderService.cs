using Bookstore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Interfaces
{
    interface IOrderService
    {
        // Buy
        public void AddToOrder(string bookTitle, string authorName, string authorSurname);
        public int IndexOfBookInOrder(string bookTitle, string authorName, string authorSurname);
        public void SubmitOrder(UserDetails userDetails);
    }
}
