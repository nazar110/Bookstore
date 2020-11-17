using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookGalleryController : Controller
    {
        IBookRepository repo;
        IOrderRepository order;
        public BookGalleryController(IBookRepository r, IOrderRepository o)
        {
            repo = r;
            order = o;
        }
        public IActionResult Index()
        {
            var books = repo.GetBookList();
            return View(books);
        }

        public IActionResult Buy(int id)
        {
            Book book = repo.GetBook(id);
            order.MakeOrder(book);
            return View();
        }
    }
}