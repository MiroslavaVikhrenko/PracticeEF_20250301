using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5_20250305
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(t => t.Hashed_Password).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(t => t.UserName).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(t => t.Salt).HasMaxLength(1024);
            modelBuilder.Entity<User>().HasIndex(t => t.UserName).IsUnique();
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Hashed_Password).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Salt).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
