using Bookstore.Core.Interfaces;
using Bookstore.Infrastructure.DTO;
using Bookstore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Infrastructure.Services
{
    public class BooksDetailsService : IBooksDetailsService
    {
        IUnitOfWork db;
        public BooksDetailsService(IUnitOfWork uow)
        {
            this.db = uow;
        }

        public IQueryable<BookBasicDetails> GetAllBooksWithBasicDetails()
        {
            IQueryable<BookBasicDetails> booksDetails = (from b in db.Books.GetAll()
                                                          join ab in db.AuthorsBooks.GetAll() on b.Id equals ab.BookId
                                                          join a in db.Authors.GetAll() on ab.AuthorId equals a.Id
                                                          join bg in db.BooksGenres.GetAll() on b.Id equals bg.BookId
                                                          join g in db.Genres.GetAll() on bg.GenreId equals g.Id
                                                          select new BookBasicDetails()
                                                          {
                                                              BookTitle = b.Title,
                                                              AuthorName = a.Name,
                                                              AuthorSurname = a.Surname,
                                                              Description = b.Description,
                                                              GenreName = g.GenreName,
                                                              Price = b.Price
                                                          }).AsQueryable();
            return booksDetails;
        }
        public BookAllDetails GetBookWithAllDetailsBy(string bookTitle, string authorName, string authorSurname)
        {
            BookAllDetails bookWithAllDetails = (from b in db.Books.GetAll()
                                                                join ab in db.AuthorsBooks.GetAll() on b.Id equals ab.BookId
                                                                join a in db.Authors.GetAll() on ab.AuthorId equals a.Id
                                                                join bg in db.BooksGenres.GetAll() on b.Id equals bg.BookId
                                                                join g in db.Genres.GetAll() on bg.GenreId equals g.Id
                                                                where b.Title == bookTitle && a.Name == authorName && a.Surname == authorSurname
                                                                select new BookAllDetails()
                                                                {
                                                                    BookTitle = b.Title,
                                                                    AuthorName = a.Name,
                                                                    AuthorSurname = a.Surname,
                                                                    Description = b.Description,
                                                                    GenreName = g.GenreName,
                                                                    Price = b.Price,
                                                                    NumberOfPages = b.NumberOfPages,
                                                                    PublicationYear = b.PublicationYear,
                                                                    PublisherName = b.PublisherName,
                                                                    Weight = b.Weight
                                                                }).FirstOrDefault();

            return bookWithAllDetails;
        }

    }
}
