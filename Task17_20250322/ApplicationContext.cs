using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task17_20250322
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // switch on lazy loading
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_BookShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Name as required
            modelBuilder.Entity<Book>().Property(g => g.Title).IsRequired();
            modelBuilder.Entity<Author>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Genre>().Property(e => e.Name).IsRequired();

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author) // Each Book must have one Author
                .WithMany(a => a.Books) // An author can have many books
                .HasForeignKey(b => b.AuthorId) // Foreign key to Author
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre) // Each Book must have one Genre
                .WithMany(g => g.Books) // A genre can have many books
                .HasForeignKey(b => b.GenreId) // Foreign key to Genre
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
