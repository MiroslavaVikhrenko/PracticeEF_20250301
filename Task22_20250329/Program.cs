using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Task22_20250329
{
    /*
     Создайте хранимую процедуру с использованием Entity Framework Core, 
    которая извлекает всех клиентов, совершивших покупку за последние 30 дней, 
    вместе с их общей суммой расходов и количеством покупок, совершенных за этот период. 
    Запрос также должен включать контактную информацию клиента, такую как имя, 
    электронная почта и номер телефона. Кроме того, результаты должны быть отсортированы по 
    общей сумме расходов в порядке убывания.

Вызовите хранимую процедуру и отобразите результат.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // Ensure database is created
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // Seed data
                SeedDatabase(db);

                // Create the stored procedure
                CreateStoredProcedure(db);

                // Call the stored procedure and display results
                CallStoredProcedure(db);
            }
        }

        // Seed the database with sample data
        static void SeedDatabase(ApplicationContext db)
        {
            var clients = new List<Client>
            {
                new Client { Name = "Yoko Yamamoto", Email = "yamamoto@example.com", PhoneNumber = "123-456-7890" },
                new Client { Name = "Taro Sato", Email = "sato@example.com", PhoneNumber = "987-654-3210" },
                new Client { Name = "Kenta Okuda", Email = "okuda@example.com", PhoneNumber = "555-555-5555" },
                new Client { Name = "Hiroo Ishizuka", Email = "ishizuka@example.com", PhoneNumber = "333-456-4890" },
                new Client { Name = "Midori Haneda", Email = "haneda@example.com", PhoneNumber = "943-687-3445" },
                new Client { Name = "Yuka Kobayashi", Email = "kobayashi@example.com", PhoneNumber = "213-789-2134" }
            };

            db.Clients.AddRange(clients);
            db.SaveChanges();

            var orders = new List<Order>
            {
                new Order { ClientId = clients[0].ClientId, TotalAmount = 120.50m, OrderDate = DateTime.UtcNow.AddDays(-5) },
                new Order { ClientId = clients[0].ClientId, TotalAmount = 80.00m, OrderDate = DateTime.UtcNow.AddDays(-15) },
                new Order { ClientId = clients[1].ClientId, TotalAmount = 200.75m, OrderDate = DateTime.UtcNow.AddDays(-10) },
                new Order { ClientId = clients[2].ClientId, TotalAmount = 50.00m, OrderDate = DateTime.UtcNow.AddDays(-2) },
                new Order { ClientId = clients[3].ClientId, TotalAmount = 150.00m, OrderDate = DateTime.UtcNow.AddDays(-40) },
                new Order { ClientId = clients[4].ClientId, TotalAmount = 90.00m, OrderDate = DateTime.UtcNow.AddDays(-89) },
                new Order { ClientId = clients[5].ClientId, TotalAmount = 20.00m, OrderDate = DateTime.UtcNow.AddDays(-100) }
            };

            db.Orders.AddRange(orders);
            db.SaveChanges();
        }

        // Create the stored procedure in SQL
        static void CreateStoredProcedure(ApplicationContext db)
        {
            string sql = @"
                CREATE OR ALTER PROCEDURE GetRecentClientsWithSpending
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT 
                        c.ClientId,
                        c.Name,
                        c.Email,
                        c.PhoneNumber,
                        COUNT(o.OrderId) AS NumberOfOrders,
                        SUM(o.TotalAmount) AS TotalSpent
                    FROM Clients c
                    JOIN Orders o ON c.ClientId = o.ClientId
                    WHERE o.OrderDate >= DATEADD(DAY, -30, GETDATE())
                    GROUP BY c.ClientId, c.Name, c.Email, c.PhoneNumber
                    ORDER BY TotalSpent DESC;
                END";

            db.Database.ExecuteSqlRaw(sql);
        }

        // Call the stored procedure and display results
        static void CallStoredProcedure(ApplicationContext db)
        {
            string sql = "EXEC GetRecentClientsWithSpending";

            using (var connection = new SqlConnection(db.Database.GetConnectionString()))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nClients who made purchases in the last 30 days:");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("ID | Name | Email | Phone | Orders | Total Spent");
                    Console.WriteLine("-------------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["ClientId"],2} | {reader["Name"],-15} | {reader["Email"],-20} | {reader["PhoneNumber"],-15} | {reader["NumberOfOrders"],3} | {reader["TotalSpent"],8:C}");
                    }
                }
            }
        }
    }
}
