using Microsoft.EntityFrameworkCore;

namespace Task25_20250401
{
    /*
     У вас есть таблица базы данных под названием «Заказы» со следующей схемой:
CREATE TABLE Orders (
    OrderId int PRIMARY KEY,
    CustomerId int NOT NULL,
    OrderDate datetime NOT NULL,
    TotalAmount decimal(10,2) NOT NULL
)

Вам необходимо написать SQL-запрос с использованием EF Core, 
    который возвращает 5 лучших клиентов, сделавших наибольшее общее количество заказов за последний месяц. 
    Можете использовать методы расширения LINQ или хранимую процедуру.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                CreateStoredProcedure(db);
                SeedDatabase(db);

                // Get top 5 customers using stored procedure
                Console.WriteLine("Top 5 Customers in the Last Month:");
                var topCustomers = GetTopCustomersLastMonth(db);

                foreach (var customer in topCustomers)
                {
                    Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.Name}, Total Orders: {customer.TotalOrders}");
                }
            }
        }

        // Execute Stored Procedure in C#
        public static List<(int Id, string Name, int TotalOrders)> GetTopCustomersLastMonth(ApplicationContext db)
        {
            // Fetch customers first 
            var topCustomers = db.Customers
                .FromSqlRaw("EXEC dbo.GetTopCustomersLastMonth")
                .ToList(); 

            // Fetch orders separately after the first query is done
            var orders = db.Orders.ToList(); 

            // Compute TotalOrders 
            return topCustomers
                .Select(c =>
                    (c.Id, c.Name, orders.Count(o => o.CustomerId == c.Id))) 
                .OrderByDescending(c => c.Item3)
                .Take(5)
                .ToList();
        }

        // Create Stored Procedure from C#
        public static void CreateStoredProcedure(ApplicationContext db)
        {
            var sql = @"
    IF OBJECT_ID('dbo.GetTopCustomersLastMonth', 'P') IS NULL
    EXEC('
    CREATE PROCEDURE dbo.GetTopCustomersLastMonth
    AS
    BEGIN
        DECLARE @StartDate DATE = DATEADD(MONTH, -1, GETDATE());

        SELECT TOP 5 c.Id, c.Name, COUNT(o.OrderId) AS TotalOrders
        FROM Customers c
        JOIN Orders o ON c.Id = o.CustomerId
        WHERE o.OrderDate >= @StartDate
        GROUP BY c.Id, c.Name
        ORDER BY COUNT(o.OrderId) DESC;
    END;')";

            db.Database.ExecuteSqlRaw(sql);
        }

        // Seed db
        public static void SeedDatabase(ApplicationContext db)
        {
            db.Customers.AddRange(new List<Customer>
            {
                new() { Name = "Yamada Tarou" }, 
                new() { Name = "Satou Hanako" }, 
                new() { Name = "Suzuki Ichirou" }, 
                new() { Name = "Tanaka Mari" }, 
                new() { Name = "Takahashi Ken" },   
                new() { Name = "Inoue Naoki" } 
            });
            db.SaveChanges();

            db.Orders.AddRange(new List<Order>
            {
                new() { CustomerId = 1, OrderDate = DateTime.Now.AddDays(-20), TotalAmount = 200 },
                new() { CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 150 },
                new() { CustomerId = 2, OrderDate = DateTime.Now.AddDays(-15), TotalAmount = 300 },
                new() { CustomerId = 3, OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 400 },
                new() { CustomerId = 3, OrderDate = DateTime.Now.AddDays(-25), TotalAmount = 500 },
                new() { CustomerId = 4, OrderDate = DateTime.Now.AddDays(-12), TotalAmount = 100 },
                new() { CustomerId = 5, OrderDate = DateTime.Now.AddDays(-7), TotalAmount = 600 },
                new() { CustomerId = 5, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 250 }
            });
            db.SaveChanges();
        }
    }
}
