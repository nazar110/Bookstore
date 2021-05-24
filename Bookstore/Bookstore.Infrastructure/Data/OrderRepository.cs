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
    public class OrderRepository : IRepository<Order>
    {
        private BookstoreContext db;

        public OrderRepository()
        {
            this.db = new BookstoreContext();
        }
        public OrderRepository(BookstoreContext context)
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

        public IEnumerable<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        public Order GetItem(int id)
        {
            return db.Orders.Find(id);
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
