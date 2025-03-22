using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task16_20250322
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // switch on lazy loading
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_FlowerBusiness;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Name as required
            modelBuilder.Entity<City>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Store>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Supplier>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(e => e.Name).IsRequired();

            modelBuilder.Entity<Store>()
                .HasOne(s => s.City) // Each Store must have one City
                .WithMany(c => c.Stores) // A city can have many stores
                .HasForeignKey(s => s.CityId) // Foreign key to City
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store) // Each Product has one Store
                .WithMany(s => s.Products) // A store can have many products
                .HasForeignKey(p => p.StoreId) // foreign key
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Store>()
                .HasMany(s => s.Suppliers)
                .WithMany(su => su.Stores);

            base.OnModelCreating(modelBuilder);
        }
    }
}
