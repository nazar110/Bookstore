using Bookstore.Helpers;
using Bookstore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.ViewModels
{
    public class BooksCatalogViewModel
    {
        public IEnumerable<BookBasicDetails> BooksDetails { get; set; }
        public string SearchedText { get; set; } = null;
        public string Filter { get; set; } = null;
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public BooksCatalogViewModel(IEnumerable<BookBasicDetails> booksDetails, int page = 1, SortState sortOrder = SortState.AuthorNameAsc, string searchedText = null, string filterBy = null)
        {
            // Search
            // make searching not vulnerable to register. Check if searches authors correctly.
            // Make searchedText visible after searching in the input.
            // When I write 'the Chronicles' should it display all 'the' and 'the Chronicles' or only 'the Chronicles'?
            // If the properties do not contain the whole text, than search the parts of that text in them
            BooksDetails = booksDetails;

            // Searching
            if (searchedText != null)
            {
                SearchedText = searchedText;
            }
            if(SearchedText != null)
            { 
                List<string> words = new List<string>();
                if (SearchedText.Contains(" "))
                {
                    (words = SearchedText.Split(' ').ToList()).ForEach(x => x.ToLower());
                }
                else
                {
                    words.Add(SearchedText.ToLower());
                }
                var query = (from bd in BooksDetails
                             from w in words
                             where bd.AuthorName.ToLower().Contains(w) || bd.AuthorSurname.ToLower().Contains(w) ||
                             bd.BookTitle.ToLower().Contains(w) || bd.GenreName.ToLower().Contains(w)
                             select bd).Distinct().ToList();

                BooksDetails = query;
            }

            // Filtering

            if (filterBy != null)
            {
                Filter = filterBy;
            }
            if(Filter != null)
            { 
                var query = (from bd in BooksDetails
                             where ($"{bd.AuthorName} {bd.AuthorSurname}" == Filter) ||
                             bd.BookTitle == Filter ||
                             bd.GenreName == Filter
                             select bd).Distinct().ToList();

                BooksDetails = query;
            }

            // Sorting
            switch (sortOrder)
            {
                case SortState.TitleAsc:
                    BooksDetails = BooksDetails.OrderBy(b => b.BookTitle).ToList();
                    break;
                case SortState.TitleDesc:
                    BooksDetails = BooksDetails.OrderByDescending(b => b.BookTitle).ToList();
                    break;
                case SortState.AuthorNameAsc:
                    BooksDetails = BooksDetails.OrderBy(b => b.AuthorName).ToList();
                    break;
                case SortState.AuthorNameDesc:
                    BooksDetails = BooksDetails.OrderByDescending(b => b.AuthorName).ToList();
                    break;
                case SortState.GenreAsc:
                    BooksDetails = BooksDetails.OrderBy(b => b.GenreName).ToList();
                    break;
                case SortState.GenreDesc:
                    BooksDetails = BooksDetails.OrderByDescending(b => b.GenreName).ToList();
                    break;
                case SortState.PriceAsc:
                    BooksDetails = BooksDetails.OrderBy(b => b.Price).ToList();
                    break;
                case SortState.PriceDesc:
                    BooksDetails = BooksDetails.OrderByDescending(b => b.Price).ToList();
                    break;
                default:
                    BooksDetails = BooksDetails.OrderBy(b => b.AuthorName).ToList();
                    break;
            }

            // Pagination
            int pageSize = 4;
            var count = BooksDetails.Count();
            var items = BooksDetails.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel = new PageViewModel(count, page, pageSize);
            BooksDetails = items;
            SortViewModel = new SortViewModel(sortOrder);
        }
        //public BooksCatalogViewModel()
        //{
        //    if (SearchedText != null)
        //    {
        //        List<string> words = new List<string>();
        //        if (SearchedText.Contains(" "))
        //        {
        //            (words = SearchedText.Split(' ').ToList()).ForEach(x => x.ToLower());
        //        }
        //        else
        //        {
        //            words.Add(SearchedText.ToLower());
        //        }
        //        var query = (from bd in BooksDetails
        //                     from w in words
        //                     where bd.AuthorName.ToLower().Contains(w) || bd.AuthorSurname.ToLower().Contains(w) ||
        //                     bd.BookTitle.ToLower().Contains(w) || bd.GenreName.ToLower().Contains(w)
        //                     select bd).Distinct().ToList();

        //        BooksDetails = query;
        //        //SearchedText = searchedText;
        //    }

        //    if (Filter != null)
        //    {
        //        var query = (from bd in BooksDetails
        //                     where ($"{bd.AuthorName} {bd.AuthorSurname}" == Filter) ||
        //                     bd.BookTitle == Filter ||
        //                     bd.GenreName == Filter
        //                     select bd).Distinct().ToList();

        //        BooksDetails = query;
        //    }
        //    switch (SortOrder)
        //    {
        //        case SortState.AuthorNameAsc:
        //            BooksDetails = BooksDetails.OrderBy(b => b.AuthorName).ToList();
        //            break;
        //        case SortState.AuthorNameDesc:
        //            BooksDetails = BooksDetails.OrderByDescending(b => b.AuthorName).ToList();
        //            break;
        //        case SortState.GenreAsc:
        //            BooksDetails = BooksDetails.OrderBy(b => b.GenreName).ToList();
        //            break;
        //        case SortState.GenreDesc:
        //            BooksDetails = BooksDetails.OrderByDescending(b => b.GenreName).ToList();
        //            break;
        //        case SortState.PriceAsc:
        //            BooksDetails = BooksDetails.OrderBy(b => b.Price).ToList();
        //            break;
        //        case SortState.PriceDesc:
        //            BooksDetails = BooksDetails.OrderByDescending(b => b.Price).ToList();
        //            break;
        //        default:
        //            BooksDetails = BooksDetails.OrderBy(b => b.AuthorName).ToList();
        //            break;
        //    }

        //    int pageSize = 4;
        //    var count = BooksDetails.Count();
        //    var items = BooksDetails.Skip((page - 1) * pageSize).Take(pageSize).ToList();


        //}
    }
}
