using Bookstore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Interfaces
{
    public interface IOrderService
    {        
        public void AddToOrder(string bookTitle, string authorName, string authorSurname);
        public int IndexOfBookInOrder(string bookTitle, string authorName, string authorSurname);
        public void SubmitOrder(UserDetails userDetails);

        public List<OrderItemDetails> GetAllItems();
        public void RemoveFromOrder(string bookTitle, string authorName, string authorSurname);
        public double GetTotalSum();
        public void SaveOrderToDB(UserDetails userDetails);
        public string CreateEmailNotification(UserDetails userDetails);
        public void SendEmailNotification(UserDetails userDetails, string message);
        public void ClearCart();
    }
}
