﻿using Bookstore.Core.EF;
using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Infrastructure.Data
{
    public class AuthorRepository : IRepository<Author>
    {
        private BookstoreContext db;

        public AuthorRepository()
        {
            this.db = new BookstoreContext();
        }
        public AuthorRepository(BookstoreContext context)
        {
            this.db = context;
        }
        public void Create(Author item)
        {
            db.Authors.Add(item);
        }

        public void Delete(int id)
        {
            var author = db.Authors.Find(id);
            if(author != null)
                db.Authors.Remove(author);
        }

        public Author GetItem(int id)
        {
            return db.Authors.Find(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return db.Authors.ToList();
        }
        public void Update(Author item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
