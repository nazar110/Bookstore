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

namespace Bookstore.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork db;
        public OrderService(IUnitOfWork uow)
        {
            this.db = uow;
        }
        // Buy
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
                cart.Add(new OrderItemDetails { Book = bookWithAllDetails, Quantity = 1 }); // Product = productModel.find(id), Quantity = 1
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
            //order.DateCreated = DateTime.Now;
            //order.UserDetails = user;
            //order.OrderItems.Add(orderItem);

            //var checkUser = db.Users.GetAll().ToList().Where(x => (x.Name == user.Name && x.Surname == user.Surname && x.Email == user.Email && x.Number == user.Number)).FirstOrDefault();
            //if (checkUser == null)
            //{
            //    db.Users.Create(new Core.Entities.User() { Name = user.Name, Surname = user.Surname, Email = user.Email, Number = user.Number, Orders = new List<Core.Entities.Order>() });
            //}

            //db.Orders.Create(new Core.Entities.Order()
            //{
            //    DateCreated = order.DateCreated
            //    //User =  //}
            //});
            //    //)
        }
        // isExist
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
        public void SubmitOrder(UserDetails userDetails)
        {
            //StringBuilder message = new StringBuilder();
            //message.Append($"<center><h2>Hello, {userDetails.Name} {userDetails.Surname}</h2></center>");
            //message.Append($"<center><h3>Bookstore thank you for your order.</h3></center>");
            //message.Append($"</br>");
            //message.Append($"<h3>Order Details</h3>");
            //message.Append($"<p>You have made order at {DateTime.Now}</p>");
            //message.Append($"<table><tr><th>Title</th><th>Author</th><th>Price</th><th>Quantity</th><th>Overall Sum</th></tr>");

            //foreach (var item in GetAllItems())
            //{
            //    message.Append($"<tr><td>{item.Book.BookTitle}</td><td>{item.Book.AuthorName} {item.Book.AuthorSurname}</td><td>{item.Book.Price}</td><td>{item.Quantity}</td><td>{item.Book.Price * item.Quantity}</td></tr>");
            //}
            //message.Append($"<tr><td></td><td></td><td></td><td>Total Sum</td><td>{GetTotalSum()}</td></tr></table>");
            //QueueStorageHelper queue = new QueueStorageHelper();
            //queue.InsertMessage("bookstoremailing", message.ToString());

            //var userId = db.Users.GetAll().Last().Id + 1;
            var user = new User()
            {
                //Id = db.Users.GetAll().Count() != 0 ? db.Users.GetAll().Last().Id + 1 : 1, // db.Users.GetAll().LastOrDefault().Id + 1,
                Name = userDetails.Name,
                Surname = userDetails.Surname,
                Email = userDetails.Email,
                Number = userDetails.Number,
                Orders = new List<Order>(),
            };


            //var id = db.Orders.GetAll().Last().Id;

            var order = new Order()
            {
                //Id = db.Orders.GetAll().Count() != 0 ? db.Orders.GetAll().Last().Id + 1 : 1,
                DateCreated = DateTime.Now,
                Items = new List<OrderItem>(), // change !
                User = user,
                UserId = user.Id
            };

            user.Orders.Add(order);
            db.Users.Create(user);

            List<OrderItem> items = new List<OrderItem>();
            foreach (var item in GetAllItems())
            {
                var book = db.Books.GetAll().Where(b => b.Title == item.Book.BookTitle && b.PublisherName == item.Book.PublisherName).FirstOrDefault();
                items.Add(new OrderItem()
                {
                    //Id = db.OrderItems.GetAll().FirstOrDefault() != null ? db.OrderItems.GetAll().FirstOrDefault().Id + 1 : 1,
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
        public List<OrderItemDetails> GetAllItems()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            return cart;
        }

        // Remove
        public void RemoveFromOrder(string bookTitle, string authorName, string authorSurname)
        {
            List<OrderItemDetails> cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            int index = IndexOfBookInOrder(bookTitle, authorName, authorSurname);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(new HttpContextAccessor().HttpContext.Session, "cart", cart);
        }
        // Total Sum
        public double GetTotalSum()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemDetails>>(new HttpContextAccessor().HttpContext.Session, "cart");
            var total = cart.Sum(item => item.Book.Price * item.Quantity);
            return (double)total;
        }
    }
}
