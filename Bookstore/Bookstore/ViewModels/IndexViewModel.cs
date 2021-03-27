using Bookstore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<BookBasicDetails> BooksDetails { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string Filter { get; set; } = null;
        public string SearchedText { get; set; } = null;
    }
}
