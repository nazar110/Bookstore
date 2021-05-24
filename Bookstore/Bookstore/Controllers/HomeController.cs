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
using Bookstore.Helpers;
using Bookstore.Infrastructure.Interfaces;
using Bookstore.Infrastructure.DTO;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBooksDetailsService BooksDetailsService;
        IOrderService OrderService;

        public HomeController(ILogger<HomeController> logger, IBooksDetailsService booksDetService, IOrderService orderService)
        {
            _logger = logger;
            BooksDetailsService = booksDetService;
            OrderService = orderService;
        }

        public IActionResult Index(int page = 1, SortState sortOrder = SortState.AuthorNameAsc, string searchedText = null, string filterBy = null)
        {
            var BooksDetails = BooksDetailsService.GetAllBooksWithBasicDetails().ToList();
            BooksCatalogViewModel viewModel = new BooksCatalogViewModel(BooksDetails, page, sortOrder, searchedText, filterBy);
            return View(viewModel);
        }
        public IActionResult Book(string bookTitle, string authorName, string authorSurname)
        {
            var bookAllDetails = BooksDetailsService.GetBookWithAllDetailsBy(bookTitle, authorName, authorSurname);
            return View(bookAllDetails);
        }

        public IActionResult AddToOrder(string bookTitle, string authorName, string authorSurname)
        {
            // <a href="/Admin/Category/Upsert?CategoryID=${item.categoryID}&CategoryName=${item.categoryName}" class="btn btn-success">Edit</a>
            OrderService.AddToOrder(bookTitle, authorName, authorSurname);
            return RedirectToAction("Book", "Home", new { bookTitle = bookTitle, authorName = authorName, authorSurname = authorSurname });
        }
        public IActionResult OrderedBooks()
        {
            var orderedItems = OrderService.GetAllItems();
            return View(orderedItems);
        }

        public IActionResult RemoveFromOrder(string bookTitle, string authorName, string authorSurname)
        {
            OrderService.RemoveFromOrder(bookTitle, authorName, authorSurname);
            return RedirectToAction("OrderedBooks", "Home");
        }
        public IActionResult SubmitOrder(UserDetails userDetails)
        {
            OrderService.SubmitOrder(userDetails);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy(int page = 1, SortState sortOrder = SortState.AuthorNameAsc, string searchedText = null, string filterBy = null)
        {
            var BooksDetails = BooksDetailsService.GetAllBooksWithBasicDetails().ToList();
            BooksCatalogViewModel viewModel = new BooksCatalogViewModel(BooksDetails, page, sortOrder, searchedText, filterBy);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
