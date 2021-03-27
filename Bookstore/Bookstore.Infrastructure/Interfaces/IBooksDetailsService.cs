using Bookstore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Infrastructure.Interfaces
{
    public interface IBooksDetailsService
    {
        public IQueryable<BookBasicDetails> GetAllBooksWithBasicDetails();
        public BookAllDetails GetBookWithAllDetailsBy(string bookTitle, string authorName, string authorSurname);
    }
}
