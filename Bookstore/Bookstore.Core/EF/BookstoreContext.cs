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

        public BookstoreContext()
        {

        }
        //public BookstoreContext(DbContextOptions options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bookstoredb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Ignore<Order>();
            modelBuilder.Entity<AuthorsBooks>().HasKey(ab => new { ab.AuthorId, ab.BookId });
            modelBuilder.Entity<AuthorsBooks>().HasOne<Author>(ab => ab.Author).WithMany(a => a.AuthorsBooks).HasForeignKey(ab => ab.AuthorId);
            modelBuilder.Entity<AuthorsBooks>().HasOne<Book>(ab => ab.Book).WithMany(b => b.AuthorsBooks).HasForeignKey(ab => ab.BookId);

            modelBuilder.Entity<BooksGenres>().HasKey(bg => new { bg.BookId, bg.GenreId });
            modelBuilder.Entity<BooksGenres>().HasOne<Book>(bg => bg.Book).WithMany(b => b.BooksGenres).HasForeignKey(bg => bg.BookId);
            modelBuilder.Entity<BooksGenres>().HasOne<Genre>(bg => bg.Genre).WithMany(g => g.BooksGenres).HasForeignKey(bg => bg.GenreId);

            modelBuilder.Entity<Order>().HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);


            //        modelBuilder.Entity<StudentCourse>()
            //.HasOne<Student>(sc => sc.Student)
            //.WithMany(s => s.StudentCourses)
            //.HasForeignKey(sc => sc.SId);


            //        modelBuilder.Entity<StudentCourse>()
            //            .HasOne<Course>(sc => sc.Course)
            //            .WithMany(s => s.StudentCourses)
            //            .HasForeignKey(sc => sc.CId);

            //modelBuilder.Entity<Ticket>().HasKey(fc => new { fc.FilmId, fc.ClientId });

            //modelBuilder.Entity<Ticket>().HasOne(t => t.Client).WithMany(c => c.Tickets).HasForeignKey(c => c.ClientId);

            //modelBuilder.Entity<Ticket>().HasOne(t => t.Film).WithMany(f => f.Tickets).HasForeignKey(f => f.FilmId);
        }
    }
}
