using Bookstore.Core.EF;
using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Services
{
    class OrderRepository
    {
        private BookstoreContext db;

        OrderRepository()
        {
            this.db = new BookstoreContext();
        }
        OrderRepository(BookstoreContext context)
        {
            this.db = context;
        }
        public void Create(Order order)
        {
            db.Orders.Add(order);
        }
        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
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
