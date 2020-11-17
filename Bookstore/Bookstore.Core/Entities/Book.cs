using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<BooksGenres> BooksGenres { get; set; }
        public List<AuthorsBooks> AuthorsBooks { get; set; }
        public decimal Price { get; set; }
    }
}
