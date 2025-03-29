using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task21_20250329
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_TrainStationSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationship using Fluent API
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Microsoft" },
                new Company { Id = 2, Name = "Google" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom Smith", Age = 30, CompanyId = 1 },
                new User { Id = 2, Name = "Alice Johnson", Age = 25, CompanyId = 2 },
                new User { Id = 3, Name = "Tom Brown", Age = 40, CompanyId = 1 }
            );
        }
    }
}
