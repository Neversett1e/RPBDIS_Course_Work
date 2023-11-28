using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication3
{
    public partial class LibraryDBContext : IdentityDbContext<IdentityUser>
    {
        public LibraryDBContext()
        {
        }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BooksOnLoan> BooksOnLoans { get; set; } = null!;
        public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; } = null!;
        public virtual DbSet<Catalog> Catalogs { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Hrdepartment> Hrdepartments { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;
        public new virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=COLORBLIND\\SQLEXPRESS; Database=LibraryDB;Integrated Security=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ_Books_ISBN")
                    .IsUnique();

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Books__GenreID__619B8048");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Books__Publisher__60A75C0F");
            });

            modelBuilder.Entity<BooksOnLoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BooksOnLoan");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.BorrowDate).HasColumnType("date");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.HasKey(e => e.BorrowId)
                    .HasName("PK__Borrowed__4295F85FDDF57E41");

                entity.Property(e => e.BorrowId).HasColumnName("BorrowID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.BorrowDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BorrowedBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BorrowedB__BookI__66603565");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.BorrowedBooks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_BorrowedBooks_Employees");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.BorrowedBooks)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__BorrowedB__Reade__6754599E");
            });

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Catalog");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Genre).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Position).HasMaxLength(100);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Hrdepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HRDepartment");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(100);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.PassportInfo).HasMaxLength(30);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
