using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Bookstore.Core.Entities;

namespace Bookstore.Core.EF
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorsBooks> AuthorsBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksGenres> BooksGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public BookstoreContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bookstoredb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorsBooks>().HasKey(ab => new { ab.AuthorId, ab.BookId });
            modelBuilder.Entity<AuthorsBooks>().HasOne<Author>(ab => ab.Author).WithMany(a => a.AuthorsBooks).HasForeignKey(ab => ab.AuthorId);
            modelBuilder.Entity<AuthorsBooks>().HasOne<Book>(ab => ab.Book).WithMany(b => b.AuthorsBooks).HasForeignKey(ab => ab.BookId);

            modelBuilder.Entity<BooksGenres>().HasKey(bg => new { bg.BookId, bg.GenreId });
            modelBuilder.Entity<BooksGenres>().HasOne<Book>(bg => bg.Book).WithMany(b => b.BooksGenres).HasForeignKey(bg => bg.BookId);
            modelBuilder.Entity<BooksGenres>().HasOne<Genre>(bg => bg.Genre).WithMany(g => g.BooksGenres).HasForeignKey(bg => bg.GenreId);

            //modelBuilder.Entity<OrdersBooks>().HasKey(ordersBooks => new { ordersBooks.OrderId, ordersBooks.BookId });
            //modelBuilder.Entity<OrdersBooks>().HasOne<Order>(ob => ob.Order).WithMany(o => o.OrdersBooks).HasForeignKey(ob => ob.OrderId);
            //modelBuilder.Entity<OrdersBooks>().HasOne<Book>(ob => ob.Book).WithMany(b => b.OrdersBooks).HasForeignKey(ob => ob.BookId);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
            modelBuilder.Entity<Order>().HasMany<OrderItem>(o => o.Items).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>().HasOne<Order>(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<OrderItem>().HasOne<Book>(oi => oi.Book).WithMany(b => b.OrderItems).HasForeignKey(oi => oi.BookId);

            modelBuilder.Entity<Book>().HasMany<OrderItem>(b => b.OrderItems).WithOne(oi => oi.Book).HasForeignKey(oi => oi.BookId);

            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().HasMany<Order>(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId);
        }
    }
}
