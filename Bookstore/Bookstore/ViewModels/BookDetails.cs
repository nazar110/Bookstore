using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.ViewModels
{
    public class BookDetails
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Description { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public BookDetails()
        {

        }
        public BookDetails(string bTitle, string aName, string aSurname, string descr, string genre, decimal price)
        {
            BookTitle = bTitle;
            AuthorName = aName;
            AuthorSurname = aSurname;
            Description = descr;
            GenreName = genre;
            Price = price;
        }
    }
}
