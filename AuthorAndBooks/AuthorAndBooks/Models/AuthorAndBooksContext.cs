using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthorAndBooks.Models;

public partial class AuthorAndBooksContext : DbContext
{
    public AuthorAndBooksContext()
    {
    }

    public AuthorAndBooksContext(DbContextOptions<AuthorAndBooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=DESKTOP-1147B1S;Database=AuthorAndBooks;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC27D181DEAA");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC271D598E97");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
	}

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
