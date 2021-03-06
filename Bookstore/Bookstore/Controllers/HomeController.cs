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

        public IActionResult Index(int page = 1, SortState sortOrder = SortState.AuthorNameAsc, string searchedText = null, string filterBy = null)
        {
            // Check if there is a need for lines 52 and 80 (ifs). Do we need that '=' mechanism?
            IndexViewModel viewModel = new IndexViewModel();

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

            // Search
            // make searching not vulnerable to register. Check if searches authors correctly.
            // Make searchedText visible after searching in the input.
            // When I write 'the Chronicles' should it display all 'the' and 'the Chronicles' or only 'the Chronicles'?
            // If the properties do not contain the whole text, than search the parts of that text in them
            if (searchedText != null)
            {
                viewModel.SearchedText = searchedText;
            }

            if (viewModel.SearchedText != null)
            {
                List<string> words = new List<string>();
                if (viewModel.SearchedText.Contains(" "))
                {
                    (words = viewModel.SearchedText.Split(' ').ToList()).ForEach(x => x.ToLower());
                }
                else
                {
                    words.Add(viewModel.SearchedText.ToLower());
                }
                var query = (from bd in booksDetails
                             from w in words
                             where bd.AuthorName.ToLower().Contains(w) || bd.AuthorSurname.ToLower().Contains(w) ||
                             bd.BookTitle.ToLower().Contains(w) || bd.GenreName.ToLower().Contains(w)
                             select bd).Distinct().ToList();

                booksDetails = query;
                viewModel.SearchedText = searchedText;
            }

            // Filtering

            if (filterBy != null)
            {
                viewModel.Filter = filterBy;
            }
            if (viewModel.Filter != null)
            {
                var query = (from bd in booksDetails
                             where ($"{bd.AuthorName} {bd.AuthorSurname}" == viewModel.Filter) ||
                             bd.BookTitle == viewModel.Filter ||
                             bd.GenreName == viewModel.Filter
                             select bd).Distinct().ToList();

                booksDetails = query;
            }

            // Sorting
            switch (sortOrder)
            {
                case SortState.AuthorNameAsc:
                    booksDetails = booksDetails.OrderBy(b => b.AuthorName).ToList();
                    break;
                case SortState.AuthorNameDesc:
                    booksDetails = booksDetails.OrderByDescending(b => b.AuthorName).ToList();
                    break;
                case SortState.GenreAsc:
                    booksDetails = booksDetails.OrderBy(b => b.GenreName).ToList();
                    break;
                case SortState.GenreDesc:
                    booksDetails = booksDetails.OrderByDescending(b => b.GenreName).ToList();
                    break;
                case SortState.PriceAsc:
                    booksDetails = booksDetails.OrderBy(b => b.Price).ToList();
                    break;
                case SortState.PriceDesc:
                    booksDetails = booksDetails.OrderByDescending(b => b.Price).ToList();
                    break;
                default:
                    booksDetails = booksDetails.OrderBy(b => b.AuthorName).ToList();
                    break;
            }

            // Pagination
            int pageSize = 4;
            var count = booksDetails.Count();
            var items = booksDetails.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            // General ViewModel initialization

            viewModel.PageViewModel = pageViewModel;
            viewModel.BooksDetails = items;
            // viewModel.FilterViewModel = new FilterViewModel(filterBy);
            viewModel.SortViewModel = new SortViewModel(sortOrder);

            return View(viewModel);
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
