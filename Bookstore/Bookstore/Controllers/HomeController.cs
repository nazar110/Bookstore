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

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBooksDetailsService BooksDetailsService;

        public HomeController(ILogger<HomeController> logger, IBooksDetailsService booksDetService)
        {
            _logger = logger;
            BooksDetailsService = booksDetService;
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
