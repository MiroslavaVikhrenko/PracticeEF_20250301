using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7_20250306
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_OrderSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Установите ограничение, чтобы сумма заказа(TotalAmount) не могла быть отрицательной. 
            modelBuilder.Entity<Order>().ToTable(o => o.HasCheckConstraint("TotalAmount", "TotalAmount > 0"));
            //Установите ограничение, чтобы дата создания(CreationDate) была установлена
            //автоматически на текущую дату при создании заказа.
            modelBuilder.Entity<Order>().Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
            //Установите ограничение, чтобы статус заказа(Status) принимал только определенные
            //значения, например: "Новый", "В обработке", "Отправлен", "Доставлен", "Отменен".
            modelBuilder.Entity<Order>().ToTable(o => o.HasCheckConstraint("Status", $"Status in ({(int)Status.New},{(int)Status.InProgress},{(int)Status.Dispatched}, {(int)Status.Delivered}, {(int)Status.Canceled})"));
            //Установите ограничение, чтобы идентификатор клиента(CustomerId) был обязательным. 
            modelBuilder.Entity<Order>().Property(o => o.CustomerId).IsRequired();
            //Установите внешний ключ между таблицами «Заказы» и «Клиенты», используя свойство "CustomerId".
            //Настройте каскадное удаление, чтобы при удалении клиента также удалялись все связанные с ним заказы.
            modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)  // Order has one Customer
            .WithMany(c => c.Orders)  // Customer has many Orders
            .HasForeignKey(o => o.CustomerId)  // Foreign Key in Orders table
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            //Убедитесь, что при обновлении заказа, его идентификатор(Id) не изменяется. 
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedNever();
            base.OnModelCreating(modelBuilder);
        }
    }
}
