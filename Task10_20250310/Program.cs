using Azure;
using Microsoft.EntityFrameworkCore;
using System;

namespace Task10_20250310
{
    /*
     Опишите 3 связанных таблицы: «Компания»,
«Магазин» и «Покупатели». Каждый магазин должен принадлежать к
определенной компании, каждый покупатель может принадлежать к нескольким
магазинам. 
 
Реализуйте возможность добавления и вывода информации, к примеру: 
 
1) Отобразите информацию обо всех компаниях.
 
Название компании, список магазинов (магазинов со своими покупателями может быть несколько), список покупателей для данного магазина.
 
2) Отобразите информацию о покупателях.
 
Фио пользователя, список магазинов и их компаний.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //Customer c1 = new Customer() { Name = "Tanaka" };
                //Customer c2 = new Customer() { Name = "Yamada" };
                //Customer c3 = new Customer() { Name = "Sato" };
                //Customer c4 = new Customer() { Name = "Fujita" };

                //Company co1 = new Company() { Name = "Canadian company" };
                //Company co2 = new Company() { Name = "American company" };
                //Company co3 = new Company() { Name = "French company" };

                //Shop s1 = new Shop() { Name = "Black Shop", Company = co1, Customers = new List<Customer>() { c1, c2 } };
                //Shop s2 = new Shop() { Name = "White Shop", Company = co1, Customers = new List<Customer>() { c2, c3 } };
                //Shop s3 = new Shop() { Name = "Green Shop", Company = co2, Customers = new List<Customer>() { c1, c3, c4 } };
                //Shop s4 = new Shop() { Name = "Blue Shop", Company = co3, Customers = new List<Customer>() { c1, c2, c4 } };
                //Shop s5 = new Shop() { Name = "Violet Shop", Company = co3, Customers = new List<Customer>() { c1, c2, c3 } };
                //Shop s6 = new Shop() { Name = "Orange Shop", Company = co3, Customers = new List<Customer>() { c1, c2, c3, c4 } };
                //db.Customers.AddRange(c1, c2, c3, c4);
                //db.Companies.AddRange(co1, co2, co3);
                //db.Shops.AddRange(s1, s2, s3, s4, s5, s6);
                //db.SaveChanges();

                // Eager loading(жадная загрузка)

                // 1) Отобразите информацию обо всех компаниях.
                // Название компании, список магазинов(магазинов со своими покупателями может быть несколько),
                // список покупателей для данного магазина.

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Company - its shops - shops' customers");
                Console.ResetColor();

                var companies = db.Companies
                .Include(c => c.Shops)
                .ThenInclude(s => s.Customers)
                .ToList();

                foreach (var company in companies)
                {
                    Console.WriteLine("..................................................");
                    Console.WriteLine($"{company.Name}:");
                    foreach (var shop in company.Shops)
                    {
                        Console.WriteLine($"-----{shop.Name}:");
                        foreach (var customer in shop.Customers)
                        {
                            Console.WriteLine($"-----------{customer.Name}");
                        }
                    }

                    //}

                    //2) Отобразите информацию о покупателях.
                    //Фио пользователя, список магазинов и их компаний.

                    Console.WriteLine("..................................................");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Customer - its shops - shops' companies");
                    Console.ResetColor();

                    var customers = db.Customers
                        .Include(c => c.Shops)
                        .ThenInclude(s => s.Company)
                        .ToList();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine("..................................................");
                        Console.WriteLine($"{customer.Name}:");
                        foreach (var shop in customer.Shops)
                        {
                            Console.WriteLine($"-----{shop.Name}:");
                            Console.WriteLine($"-----------{shop.Company.Name}");
                        }

                    }
                }
            }
        }

        
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shop> Shops { get; set; } // nav prop
    }
    public class Shop
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int CompanyId { get; set; } // foreign key
        public Company Company { get; set; } //nav prop
        public List<Customer> Customers { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shop> Shops { get; set; } //nav prop
    }
}
