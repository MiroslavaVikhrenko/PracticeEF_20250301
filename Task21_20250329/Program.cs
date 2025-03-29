using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using System;

namespace Task21_20250329
{
    /*
     Используя таблицы «Users» и «Companies», создать 3 хранимых процедуры:
Получение связанных данных о Users и Companies.
Используя входной параметр получить пользователей с именем наподобие “Tom”.
Используя выходной параметр, получить средний возраст по всей таблицы пользователей.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Creating stored procedures...");
                CreateStoredProcedures(db);

                Console.WriteLine("Testing stored procedures...\n");

                // Fetch Users with their Companies
                FetchUsersWithCompanies();

                // Get Users with name like 'Tom'
                GetUsersByNamePattern("Tom");

                // Get Average Age
                GetAverageAge();
            }

        }
        public static void GetAverageAge()
        {
            Console.WriteLine("\n--- Average Age of Users ---");

            using (var db = new ApplicationContext())
            using (var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetAverageUserAge", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Define output parameter
                    var avgAgeParam = new SqlParameter("@AvgAge", System.Data.SqlDbType.Float)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };

                    command.Parameters.Add(avgAgeParam);
                    command.ExecuteNonQuery();

                    // Read the output parameter
                    double avgAge = (double)command.Parameters["@AvgAge"].Value;
                    Console.WriteLine($"Average Age: {avgAge:F2}");
                }
            }
        }
        public static void GetUsersByNamePattern(string namePattern)
        {
            Console.WriteLine($"\n--- Users with name like '{namePattern}' ---");

            using (var db = new ApplicationContext())
            using (var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetUsersByNamePattern", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NamePattern", namePattern);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"User: {reader["Name"]} | Age: {reader["Age"]}");
                        }
                    }
                }
            }
        }
        public static void FetchUsersWithCompanies()
        {
            Console.WriteLine("\n--- Users and Their Companies ---");

            using (var db = new ApplicationContext())
            using (var connection = new SqlConnection(db.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetUsersWithCompanies", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"User: {reader["Name"]} | Age: {reader["Age"]} | Company: {reader["CompanyName"]}");
                        }
                    }
                }
            }
        }
        public static void CreateStoredProcedures(ApplicationContext db)
        {
            db.Database.ExecuteSqlRaw(@"
        CREATE PROCEDURE dbo.GetUsersWithCompanies
        AS
        BEGIN
            SELECT u.Id, u.Name, u.Age, c.Name AS CompanyName
            FROM Users u
            INNER JOIN Companies c ON u.CompanyId = c.Id;
        END;");

            db.Database.ExecuteSqlRaw(@"
        CREATE PROCEDURE dbo.GetUsersByNamePattern
            @NamePattern NVARCHAR(100)
        AS
        BEGIN
            SELECT * FROM Users WHERE Name LIKE '%' + @NamePattern + '%';
        END;");

            db.Database.ExecuteSqlRaw(@"
        CREATE PROCEDURE dbo.GetAverageUserAge
            @AvgAge FLOAT OUTPUT
        AS
        BEGIN
            SELECT @AvgAge = AVG(CAST(Age AS FLOAT)) FROM Users;
        END;");

            Console.WriteLine("Stored procedures created successfully.");
        }
    }
}
