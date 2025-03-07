using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_20250306
{
    public  class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_ProductsSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Убедитесь, что свойства имеют соответствующие типы данных в базе данных. 
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Product>().Property(p => p.Category).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnType("nvarchar(250)");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
            //Используйте составной ключ.
            modelBuilder.Entity<Product>().HasKey(p => new { p.Id, p.Name });
            //Установите ограничение, чтобы название продукта(Name) было обязательным и не превышало 100 символов.
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("Name", "LEN(Name) <= 100"));
            //Установите ограничение, чтобы цена продукта(Price) была больше 0.
            modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("Price", "Price > 0"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
