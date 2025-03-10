using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10_20250310
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_ShopCustomerCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<Shop>()
            .HasOne(s => s.Company)  // Shop has one Company
            .WithMany(c => c.Shops)  // Company has many Shops
            .HasForeignKey(s => s.CompanyId)  // Foreign Key in Shops table
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Shops)
                .WithMany(s => s.Customers);

            base.OnModelCreating(modelBuilder);
        }
    }
}
