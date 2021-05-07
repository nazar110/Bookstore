using Bookstore.Core.EF;
using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Data
{
    class UserRepository : IRepository<User>
    {
        private BookstoreContext db;

        public UserRepository()
        {
            this.db = new BookstoreContext();
        }
        public UserRepository(BookstoreContext context)
        {
            this.db = context;
        }
        public void Create(User order)
        {
            db.Users.Add(order);
        }
        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
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

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User GetItem(int id)
        {
            return db.Users.Find(id);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
