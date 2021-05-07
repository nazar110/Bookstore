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
            BooksDetails = SearchInCollection(BooksDetails, SearchedText, searchedText);

            // Filtering
            BooksDetails = FilteredCollection(BooksDetails, Filter, filterBy);

            // Sorting
            BooksDetails = SortedCollection(BooksDetails, sortOrder);

            // Pagination
            int pageSize = 4;
            var count = BooksDetails.Count();
            var items = BooksDetails.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel = new PageViewModel(count, page, pageSize);
            BooksDetails = items;
            SortViewModel = new SortViewModel(sortOrder);
        }

        private IEnumerable<BookBasicDetails> SearchInCollection(IEnumerable<BookBasicDetails> booksDetails, string currentSearchedText, string newSearchedText)
        {
            if (newSearchedText != null)
            {
                currentSearchedText = newSearchedText;
            }
            if (currentSearchedText != null)
            {
                List<string> words = new List<string>();
                if (currentSearchedText.Contains(" "))
                {
                    words = currentSearchedText.Split(' ').Select(x => x.ToLower()).ToList();
                }
                else
                {
                    words.Add(currentSearchedText.ToLower());
                }
                var query = (from bd in booksDetails
                             from w in words
                             where bd.AuthorName.ToLower().Contains(w) || bd.AuthorSurname.ToLower().Contains(w) ||
                             bd.BookTitle.ToLower().Contains(w) || bd.GenreName.ToLower().Contains(w)
                             select bd).Distinct().ToList();

                booksDetails = query;
            }
            return booksDetails;
        }
        private IEnumerable<BookBasicDetails> FilteredCollection(IEnumerable<BookBasicDetails> booksDetails, string currentFilter, string newFilter)
        {
            if (newFilter != null)
            {
                currentFilter = newFilter;
            }
            if (currentFilter != null)
            {
                var query = (from bd in booksDetails
                             where ($"{bd.AuthorName} {bd.AuthorSurname}" == currentFilter) ||
                             bd.BookTitle == currentFilter ||
                             bd.GenreName == currentFilter
                             select bd).Distinct().ToList();

                booksDetails = query;
            }
            return booksDetails;
        }
        private IEnumerable<BookBasicDetails> SortedCollection(IEnumerable<BookBasicDetails> booksDetails, SortState sortOrder)
        {
            switch (sortOrder)
            {
                case SortState.TitleAsc:
                    booksDetails = booksDetails.OrderBy(b => b.BookTitle).ToList();
                    break;
                case SortState.TitleDesc:
                    booksDetails = booksDetails.OrderByDescending(b => b.BookTitle).ToList();
                    break;
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
            return booksDetails;
        }

    }
}
