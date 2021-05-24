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
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private BookstoreContext db;

        public OrderItemRepository()
        {
            this.db = new BookstoreContext();
        }
        public OrderItemRepository(BookstoreContext context)
        {
            this.db = context;
        }
        public void Create(OrderItem item)
        {
            db.OrderItems.Add(item);
        }

        public void Delete(int id)
        {
            var orderItem = db.OrderItems.Find(id);
            if (orderItem != null)
                db.OrderItems.Remove(orderItem);
        }

        public OrderItem GetItem(int id)
        {
            return db.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return db.OrderItems.ToList();
        }
        public void Update(OrderItem item)
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
