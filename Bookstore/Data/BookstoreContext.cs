using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Bookstore.Models;
using Microsoft.Extensions.Configuration;

namespace Bookstore.Data
{
    public partial class BookstoreContext : DbContext
    {
        public BookstoreContext()
        {
        }

        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<book> Books { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<VTitlesPerAuthor> VTitlesPerAuthors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bookstore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasMany(d => d.Isbn13s)
                    .WithMany(p => p.Authors)
                    .UsingEntity<Dictionary<string, object>>(
                        "AuthorsBook",
                        l => l.HasOne<book>().WithMany().HasForeignKey("Isbn13").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Authors_Books_Books"),
                        r => r.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Authors_Books_Authors"),
                        j =>
                        {
                            j.HasKey("AuthorId", "Isbn13");

                            j.ToTable("Authors_Books");

                            j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");

                            j.IndexerProperty<long>("Isbn13").HasColumnName("ISBN13");
                        });
            });

            modelBuilder.Entity<book>(entity =>
            {
                entity.Property(e => e.Isbn13).ValueGeneratedNever();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_Addresses");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.Isbn13 });

                entity.HasOne(d => d.Isbn13Navigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.Isbn13)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocks_Books");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocks_Stores");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Stores_Addresses");
            });

            modelBuilder.Entity<VTitlesPerAuthor>(entity =>
            {
                entity.ToView("v_TitlesPerAuthor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
