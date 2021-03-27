using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.DTO
{
    public class BookAllDetails
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Description { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public int PublicationYear { get; set; }
        public string PublisherName { get; set; }
        public double Weight { get; set; }
    }
}
