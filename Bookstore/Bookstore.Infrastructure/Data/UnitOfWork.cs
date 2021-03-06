﻿using Bookstore.Core.EF;
using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookstoreContext db = new BookstoreContext();
        private AuthorRepository authorRepository;
        private AuthorsBooksRepository authorsBooksRepository;
        private BookRepository bookRepository;
        private BooksGenresRepository booksGenresRepository;
        private GenreRepository genreRepository;
        private OrderRepository orderRepository;
        private UserRepository userRepository;
        private OrderItemRepository orderItemRepository;

        public IRepository<Author> Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }
        public IRepository<AuthorsBooks> AuthorsBooks
        {
            get
            {
                if (authorsBooksRepository == null)
                    authorsBooksRepository = new AuthorsBooksRepository(db);
                return authorsBooksRepository;
            }
        }
        public IRepository<Book> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }
        public IRepository<BooksGenres> BooksGenres
        {
            get
            {
                if (booksGenresRepository == null)
                    booksGenresRepository = new BooksGenresRepository(db);
                return booksGenresRepository;
            }
        }
        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }
        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (orderItemRepository == null)
                    orderItemRepository = new OrderItemRepository(db);
                return orderItemRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
