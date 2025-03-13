using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12_20250313
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_Projects_EC;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<Project>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Company)   // Employee has one Company
            .WithMany(c => c.Employees)        // Company has several Employees
            .HasForeignKey(e => e.CompanyId)  // Foreign Key is CompanyId in Employees table
            .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            base.OnModelCreating(modelBuilder);
        }
    }
}
