using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task2_practice_fluent_API
{
    internal class Program
    {
        /*
         Вам необходимо создать модель данных для интернет-магазина 
        и настроить ее с помощью Fluent API. Выполните следующие шаги:

Создайте модель Product с полями:
Id (целочисленный, первичный ключ)
Name (строка, обязательное поле, макс. 100 символов)
Price (десятичное число с двумя знаками после запятой)
StockQuantity (целое число, по умолчанию 0)
Description (текст, необязательное поле)

1) Настройте первичный ключ для Id с помощью HasKey().
2) Установите ограничение длины строки для Name (максимум 100 символов) с HasMaxLength().
3) Сделайте поле Name обязательным с помощью IsRequired().
4) Настройте Price: Должно храниться как decimal(10,2).
5) Установите значение по умолчанию для StockQuantity (0).
6) Сделайте поле Description необязательным (IsRequired(false)).
7) Запретите дублирование Name (уникальный индекс).
8) Игнорируйте поле TemporaryData, если оно есть в модели.
9) Задайте имя таблицы в базе данных как StoreProducts.
10) Добавьте ограничение: Price не может быть отрицательным (HasCheckConstraint()). 
         */
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Product> products = new List<Product>
                {
                    new Product{Name = "Table", Price = 30m, StockQuantity = 5, Description = "Japanese table", TemporaryData = "hjgj"},
                    new Product{Name = "Chair", Price = 25m, StockQuantity = 15, Description = "Vietnamese chair"}
                };
                db.Products.AddRange(products);
                db.SaveChanges();
            }
        }
    }
    //Запретите дублирование Name(уникальный индекс).
    //[Index("Name", IsUnique = true, Name = "Name_Index")]
    public class Product
    {
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //Настройте Price: Должно храниться как decimal(10, 2).
        [Precision(10,2)]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? TemporaryData { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=EShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Настройте первичный ключ для Id с помощью HasKey()
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            //Установите ограничение длины строки для Name(максимум 100 символов) с HasMaxLength().
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100);
            //Сделайте поле Name обязательным с помощью IsRequired().
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            //Установите значение по умолчанию для StockQuantity(0).
            modelBuilder.Entity<Product>().Property(p => p.StockQuantity).HasDefaultValue(0);
            //Сделайте поле Description необязательным(IsRequired(false)).
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired(false);
            //Запретите дублирование Name(уникальный индекс).
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
            //Игнорируйте поле TemporaryData, если оно есть в модели.
            modelBuilder.Entity<Product>().Ignore(p => p.TemporaryData);
            //Задайте имя таблицы в базе данных как StoreProducts.
            modelBuilder.Entity<Product>().ToTable("StoreProducts");
            //Добавьте ограничение: Price не может быть отрицательным(HasCheckConstraint()).
            modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("Price", "Price > 0"));
            //modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(10,2)");
            base.OnModelCreating(modelBuilder);
        }
    }
}
