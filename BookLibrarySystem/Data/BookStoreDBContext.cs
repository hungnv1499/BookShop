using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data
{
    public class BookStoreDBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base (options)
        {

        }
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Authors>().ToTable("Authors")
                .HasIndex(i => i.AuthorName).IsUnique();
            modelBuilder.Entity<Books>().ToTable("Books")
                .HasIndex(i => i.BookName).IsUnique();
            modelBuilder.Entity<Brands>().ToTable("Brands")
                .HasIndex(i => i.BrandName).IsUnique();
            modelBuilder.Entity<Categories>().ToTable("Categories")
                .HasIndex(i => i.CategoryName).IsUnique();
            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<Payments>().ToTable("Payments");
        }
    }
}
