using Microsoft.EntityFrameworkCore;

namespace Task23_20250331
{
    /*
     Напишите хранимую функцию с использованием Entity Framework Core, которая вычисляет общий доход, 
    полученный от определенной категории товаров за заданный диапазон дат. Функция должна принимать в качестве 
    параметров ID категории, дату начала и дату окончания и возвращать общую выручку в виде десятичного значения. 
    Кроме того, функция должна учитывать только те заказы, которые были отправлены и не отменены. Затем функция 
    должна быть отображена на функцию EF Core и доступна через LINQ-запросы.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                CreateSqlFunction(db);
                SeedDatabase(db);

                int categoryId = 1;
                DateTime startDate = DateTime.Now.AddMonths(-1);
                DateTime endDate = DateTime.Now;

                decimal revenue = db.Categories
                    .Where(c => c.Id == categoryId)
                    .Select(c => db.GetTotalRevenue(categoryId, startDate, endDate))
                    .FirstOrDefault();

                Console.WriteLine($"Total Revenue for Category {categoryId} from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {revenue}");
            }             
        }

        // Create SQL Function From C#
        public static void CreateSqlFunction(ApplicationContext db)
        {
            var sql = @"
    IF OBJECT_ID('dbo.GetTotalRevenue', 'FN') IS NULL
    EXEC('
    CREATE FUNCTION dbo.GetTotalRevenue (@CategoryId INT, @StartDate DATE, @EndDate DATE)
    RETURNS DECIMAL(18,2)
    AS
    BEGIN
        DECLARE @TotalRevenue DECIMAL(18,2);

        SELECT @TotalRevenue = SUM(od.Quantity * p.Price)
        FROM OrderDetails od
        JOIN Orders o ON od.OrderId = o.Id
        JOIN Products p ON od.ProductId = p.Id
        WHERE p.CategoryId = @CategoryId
            AND o.ShippedDate IS NOT NULL
            AND o.IsCanceled = 0
            AND o.OrderDate BETWEEN @StartDate AND @EndDate;

        RETURN ISNULL(@TotalRevenue, 0);
    END;')";

            db.Database.ExecuteSqlRaw(sql);
        }

        // Seed db
        public static void SeedDatabase(ApplicationContext db)
        {
            Console.WriteLine("Seeding database...");

            db.Categories.AddRange(new List<Category>
            {
                new() { Name = "Electronics" },
                new() { Name = "Clothing" }
            });
            db.SaveChanges();

            db.Products.AddRange(new List<Product>
            {
                new() { Name = "Laptop", Price = 1200, CategoryId = 1 },
                new() { Name = "Smartphone", Price = 800, CategoryId = 1 },
                new() { Name = "T-Shirt", Price = 20, CategoryId = 2 }
            });
            db.SaveChanges();

            db.Orders.AddRange(new List<Order>
            {
                new() {OrderDate = DateTime.Now.AddDays(-10), ShippedDate = DateTime.Now.AddDays(-7), IsCanceled = false },
                new() {OrderDate = DateTime.Now.AddDays(-5), ShippedDate = null, IsCanceled = false }
            });
            db.SaveChanges();

            db.OrderDetails.AddRange(new List<OrderDetail>
            {
                new() { OrderId = 1, ProductId = 1, Quantity = 2 },
                new() { OrderId = 1, ProductId = 2, Quantity = 1 }
            });

            db.SaveChanges();
            Console.WriteLine("Database seeded successfully.");
        }

    }
}
