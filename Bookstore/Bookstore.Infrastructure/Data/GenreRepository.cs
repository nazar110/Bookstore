using Bookstore.Core.EF;
using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Infrastructure.Data
{
    public class GenreRepository : IRepository<Genre>
    {
        private BookstoreContext db;
        public GenreRepository()
        {
            this.db = new BookstoreContext();
        }
        public GenreRepository(BookstoreContext context)
        {
            this.db = context;
        }
        public void Create(Genre item)
        {
            db.Genres.Add(item);
        }

        public void Delete(int id)
        {
            var genre = db.Genres.Find(id);
            if (genre != null)
                db.Genres.Remove(genre);
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

        public IEnumerable<Genre> GetAll()
        {
            return db.Genres.ToList();
        }

        public Genre GetItem(int id)
        {
            return db.Genres.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Genre item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
