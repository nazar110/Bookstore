using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<BooksGenres> BooksGenres { get; set; }
    }
}
