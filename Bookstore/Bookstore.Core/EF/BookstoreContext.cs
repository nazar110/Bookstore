using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Bookstore.Core.Entities;

namespace Bookstore.Core.EF
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BookstoreContext()
        {

        }
        //public BookstoreContext(DbContextOptions options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=cinemadb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Ticket>().HasKey(fc => new { fc.FilmId, fc.ClientId });

            //modelBuilder.Entity<Ticket>().HasOne(t => t.Client).WithMany(c => c.Tickets).HasForeignKey(c => c.ClientId);

            //modelBuilder.Entity<Ticket>().HasOne(t => t.Film).WithMany(f => f.Tickets).HasForeignKey(f => f.FilmId);
        }
    }
}
