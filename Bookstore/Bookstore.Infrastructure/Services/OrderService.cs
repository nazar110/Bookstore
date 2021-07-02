using AutoMapper;
using Bookstore.Core.Helpers;
using Bookstore.Core.Interfaces;
using Bookstore.Core.Entities;
using Bookstore.Infrastructure.DTO;
using Bookstore.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Bookstore.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork db;
        public OrderService(IUnitOfWork uow)
        {
            this.db = uow;
        }
        public void AddToOrder(string bookTitle, string authorName, string authorSurname)
        {
            BookAllDetails bookWithAllDetails = (from b in db.Books.GetAll()
                                                 join ab in db.AuthorsBooks.GetAll() on b.Id equals ab.BookId
                                                 join a in db.Authors.GetAll() on ab.AuthorId equals a.Id
                                                 join bg in db.BooksGenres.GetAll() on b.Id equals bg.BookId
                                                 join g in db.Genres.GetAll() on bg.GenreId equals g.Id
                                                 where b.Title == bookTitle && a.Name == authorName && a.Surname == authorSurname
                                                 select new BookAllDetails()
                                                 {
                                                     BookTitle = b.Title,
                                                     AuthorName = a.Name,
                                                     AuthorSurname = a.Surname,
                                                     Description = b.Description,
                                                     GenreName = g.GenreName,
                                                     Price = b.Price,
                                                     NumberOfPages = b.NumberOfPages,
                                                     PublicationYear = b.PublicationYear,
                                                     PublisherName = b.PublisherName,
                                                     Weight = b.Weight
                                                 }).FirstOrDefault();
            if (SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart") == null)
            {
                List<OrderItemDetails> cart = new List<OrderItemDetails>();
                cart.Add(new OrderItemDetails { Book = bookWithAllDetails, Quantity = 1 });
                SessionHelper.SetObjectAsJson(new HttpContextAccessor().HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderItemDetails> cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
                int index = IndexOfBookInOrder(bookTitle, authorName, authorSurname);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new OrderItemDetails { Book = bookWithAllDetails, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(new HttpContextAccessor().HttpContext.Session, "cart", cart);
            }
        }
        public int IndexOfBookInOrder(string bookTitle, string authorName, string authorSurname)
        {
            List<OrderItemDetails> cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Book.BookTitle == bookTitle && cart[i].Book.AuthorName == authorName && cart[i].Book.AuthorSurname == authorSurname)
                {
                    return i;
                }
            }
            return -1;
        }
        //public void SubmitOrder(UserDetails userDetails)
        //{
        //    //StringBuilder message = new StringBuilder();
        //    //message.Append($"<center><h2>Hello, {userDetails.Name} " +
        //    //    $"{userDetails.Surname}</h2></center>");
        //    //message.Append($"<center><h3>Bookstore thank you for " +
        //    //    $"your order.</h3></center>");
        //    //message.Append($"</br>");
        //    //message.Append($"<h3>Order Details</h3>");
        //    //message.Append($"<p>You have made order at {DateTime.Now}</p>");
        //    //message.Append($"<table><tr><th>Title</th><th>Author</th><th>" +
        //    //    $"Price</th><th>Quantity</th><th>Overall Sum</th></tr>");

        //    //foreach (var item in GetAllItems())
        //    //{
        //    //    message.Append($"<tr><td>{item.Book.BookTitle}</td><td>" +
        //    //        $"{item.Book.AuthorName} {item.Book.AuthorSurname}</td>" +
        //    //        $"<td>{item.Book.Price}</td><td>{item.Quantity}</td><td>" +
        //    //        $"{item.Book.Price * item.Quantity}</td></tr>");
        //    //}
        //    //message.Append($"<tr><td></td><td></td><td></td><td>Total Sum" +
        //    //    $"</td><td>{GetTotalSum()}</td></tr></table>");
        //    //QueueStorageHelper queue = new QueueStorageHelper();
        //    //queue.InsertMessage("bookstoremailing", message.ToString());

        //    var user = new User()
        //    {
        //        Name = userDetails.Name,
        //        Surname = userDetails.Surname,
        //        Email = userDetails.Email,
        //        Number = userDetails.Number,
        //        Orders = new List<Order>(),
        //    };

        //    var order = new Order()
        //    {
        //        DateCreated = DateTime.Now,
        //        Items = new List<OrderItem>(),
        //        User = user,
        //        UserId = user.Id
        //    };

        //    user.Orders.Add(order);
        //    db.Users.Create(user);

        //    List<OrderItem> items = new List<OrderItem>();
        //    foreach (var item in GetAllItems())
        //    {
        //        var book = db.Books.GetAll().Where(b => b.Title == item.Book.BookTitle && 
        //        b.PublisherName == item.Book.PublisherName).FirstOrDefault();
        //        items.Add(new OrderItem()
        //        {
        //            BookId = book.Id,
        //            Book = book,
        //            Quantity = item.Quantity,
        //            OrderId = order.Id,
        //            Order = order
        //        }
        //        );
        //    }

        //    foreach (var orderItem in items)
        //    {
        //        db.OrderItems.Create(orderItem);
        //    }
        //    order.Items.AddRange(items);
        //    db.Orders.Create(order);

        //    db.Save();
        //}
        public void SaveOrderToDB(UserDetails userDetails)
        {
            var user = new User()
            {
                Name = userDetails.Name,
                Surname = userDetails.Surname,
                Email = userDetails.Email,
                Number = userDetails.Number,
                Orders = new List<Order>(),
            };

            var order = new Order()
            {
                DateCreated = DateTime.Now,
                Items = new List<OrderItem>(),
                User = user,
                UserId = user.Id
            };

            user.Orders.Add(order);
            db.Users.Create(user);

            List<OrderItem> items = new List<OrderItem>();
            foreach (var item in GetAllItems())
            {
                var book = db.Books.GetAll().Where(b => b.Title == item.Book.BookTitle &&
                b.PublisherName == item.Book.PublisherName).FirstOrDefault();
                items.Add(new OrderItem()
                {
                    BookId = book.Id,
                    Book = book,
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                    Order = order
                }
                );
            }

            foreach (var orderItem in items)
            {
                db.OrderItems.Create(orderItem);
            }
            order.Items.AddRange(items);
            db.Orders.Create(order);

            db.Save();
        }
        public string CreateEmailNotification(UserDetails userDetails)
        {
            StringBuilder message = new StringBuilder();
            message.Append($"<center><h2>Hello, {userDetails.Name} " +
                $"{userDetails.Surname}</h2></center><center><h3>Bookstore thank you for your order.</h3></center>" +
            $"</br><h3>Order Details</h3><p>You have made order at {DateTime.Now}</p>" +
            $"<table><tr><th>Title</th><th>Author</th><th>Price</th><th>Quantity</th><th>Overall Sum</th></tr>");

            foreach (var item in GetAllItems())
            {
                message.Append($"<tr><td>{item.Book.BookTitle}</td><td>" +
                    $"{item.Book.AuthorName} {item.Book.AuthorSurname}</td>" +
                    $"<td>{item.Book.Price}</td><td>{item.Quantity}</td><td>" +
                    $"{item.Book.Price * item.Quantity}</td></tr>");
            }
            message.Append($"<tr><td></td> <td></td> <td></td> <td></td> <td>Total: {GetAllItems().Sum(item => item.Book.Price * item.Quantity)}</td></tr>");
            return message.ToString();
        }
        public async void SendEmailNotification(UserDetails userDetails, string message)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://courseprojectemailsendingtask.azurewebsites.net/api/HttpTrigger2?code=l8JQcdqLlp/b8dgF9xTi3yzw584Sabm9qNtn4WwmcagkPxB60U0dNg==" +
                $"&name={userDetails.Name}&surname={userDetails.Surname}&email={userDetails.Email}&message={message}");
        }
        public void ClearCart()
        {
            SessionHelper.SetObjectAsJson(new HttpContextAccessor().HttpContext.Session, "cart", new List<OrderItemDetails>());
        }
        public void SubmitOrder(UserDetails userDetails)
        {
            SaveOrderToDB(userDetails);
            string message = CreateEmailNotification(userDetails);
            SendEmailNotification(userDetails, message);
            ClearCart();
        }
        public List<OrderItemDetails> GetAllItems()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            return cart;
        }
        public void RemoveFromOrder(string bookTitle, string authorName, string authorSurname)
        {
            List<OrderItemDetails> cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>
                (new HttpContextAccessor().HttpContext.Session, "cart");
            int index = IndexOfBookInOrder(bookTitle, authorName, authorSurname);
            //cart.RemoveAt(index);
            if (cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(new HttpContextAccessor().HttpContext.Session, "cart", cart);
        }
        public double GetTotalSum()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            var total = cart.Sum(item => item.Book.Price * item.Quantity);
            return (double)total;
        }
    }
}
