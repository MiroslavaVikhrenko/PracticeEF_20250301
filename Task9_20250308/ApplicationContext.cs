using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_20250308
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_GameShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Game>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Game>()
            .HasOne(g => g.Shop)  // Game has one Shop
            .WithMany(s => s.Games)  // Shop has many Games
            .HasForeignKey(g => g.ShopId)  // Foreign Key in Games table
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            base.OnModelCreating(modelBuilder);
        }
    }
}
