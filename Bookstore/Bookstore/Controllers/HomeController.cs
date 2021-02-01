using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookstore.Models;
using Bookstore.Core.Interfaces;
using Bookstore.ViewModels;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUnitOfWork db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            db = uow;
        }

        public IActionResult Index()
        {
            var booksDetails = (from b in db.Books.GetAll()
                                join ab in db.AuthorsBooks.GetAll() on b.Id equals ab.BookId
                                join a in db.Authors.GetAll() on ab.AuthorId equals a.Id
                                join bg in db.BooksGenres.GetAll() on b.Id equals bg.BookId
                                join g in db.Genres.GetAll() on bg.GenreId equals g.Id
                                select new BookDetails()
                                {
                                    BookTitle = b.Title,
                                    AuthorName = a.Name,
                                    AuthorSurname = a.Surname,
                                    Description = b.Description,
                                    GenreName = g.GenreName,
                                    Price = b.Price
                                }).
            ToList();

            return View(booksDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
