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
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_UserCompanyMgmtSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set required props
            modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();

            // User - Company relations
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company) // Each User must have one Company
                .WithMany(c => c.Users) // A company can have many users
                .HasForeignKey(u => u.CompanyId) // Foreign key to Company
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
