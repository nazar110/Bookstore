﻿using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Author> Authors { get; }
        public IRepository<AuthorsBooks> AuthorsBooks { get; }
        public IRepository<Book> Books { get; }
        public IRepository<BooksGenres> BooksGenres { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<User> Users { get; }
        public IRepository<OrderItem> OrderItems { get; }
        public void Save();

    }
}
