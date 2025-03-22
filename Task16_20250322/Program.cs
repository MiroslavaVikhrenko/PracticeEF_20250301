using Microsoft.EntityFrameworkCore;

namespace Task16_20250322
{
    /*
     Владельцу сети «Цветочных магазинов», 
    требуется программа для управления его бизнесом. 
    Опишите классы и на основе классов таблицы: «Город», «Магазин», «Поставщик», «Товар». 

Каждый цветочный магазин расположен в определенном городе и имеет адрес. 
    У каждого цветочного магазина может быть один или несколько поставщиков и 
    коллекция товаров (цветы, упаковочные коробки и т.д.). 
Для каждого класса создается отдельная таблица. Реализовать следующие возможности:

1) Поиск  товара по определенному магазину (Используя оператор LIKE in SQL).
2) Поиск товара по всем магазинам.
(Используя оператор LIKE in SQL).
3) Получение случайного товара из определенного магазина.
4) Сортировка цветов: по убыванию и возрастанию их стоимости.
5) На основе анонимного типа, сформировать отчет, содержащий: общее количество товара 
    в наличии и общую сумму товара для каждого из магазинов в определенном городе (выбор города по ID).
    6) Вывести для владельца общую информацию о его бизнесе  виде:

-- Город
   -- Магазин 1
      -- Товары: ...
   -- Магазин 2
      -- Товары: …

 и т.д..

7) Вывести товары из определенного магазина, сумма которых превосходит N гривен.
8) Вывести названия магазинов, у которых количество превосходит N единиц.
9) Реализовать получение магазинов определенного города через Свойство Shops, в модели Country.
10) Получить среднюю стоимость по каждому магазину из разных городов, по типу:
Киев:
Магазин "Цветочный веник": Средняя стоимость : 155
Магазин "Феерия": Средняя стоимость : 172
Днепр:
Магазин "Тюльпанчик": Средняя стоимость : 188
Магазин "У бабушки Гали": Средняя стоимость : 177
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<City> cities = new List<City>()
                {
                    new City {Name = "Calgary"},
                    new City {Name = "Edmonton"},
                    new City {Name = "Red Deer"}
                };
                db.Cities.AddRange(cities);
                db.SaveChanges();

                List<Supplier> suppliers = new List<Supplier>()
                {
                    new Supplier{Name = "Supplier 1"},
                    new Supplier{Name = "Supplier 2"},
                    new Supplier{Name = "Supplier 3"}
                };
                db.Suppliers.AddRange(suppliers);
                db.SaveChanges();

                List<Store> stores = new List<Store>()
                {
                    new Store() { Name = "Store 1", CityId = 1, Address = "190-289", Suppliers = new List<Supplier>(){suppliers[0], suppliers[1] } },
                    new Store() { Name = "Store 2", CityId = 1, Address = "241-234", Suppliers = new List<Supplier>(){suppliers[1], suppliers[2] } },
                    new Store() { Name = "Store 3", CityId = 2, Address = "129-567", Suppliers = new List<Supplier>(){suppliers[0], suppliers[2] } },
                    new Store() { Name = "Store 4", CityId = 3, Address = "309-222", Suppliers = new List<Supplier>(){suppliers[0], suppliers[1], suppliers[2] } },
                    new Store() { Name = "Store 5", CityId = 3, Address = "987-123", Suppliers = new List<Supplier>(){suppliers[1] } }
                };
                db.Stores.AddRange(stores);
                db.SaveChanges();

                List<Product> products = new List<Product>()
                {
                    new Product(){Name = "Roses", StoreId = 1, Price = 10, Amount = 2},
                    new Product(){Name = "Lilies", StoreId = 1, Price = 5, Amount = 3},
                    new Product(){Name = "Peonies", StoreId = 2, Price = 6, Amount = 4},
                    new Product(){Name = "Azisai", StoreId = 2, Price = 9, Amount = 7},
                    new Product(){Name = "Camomiles", StoreId = 3, Price = 2, Amount = 10},
                    new Product(){Name = "Sakura", StoreId = 4, Price = 11, Amount = 2},
                    new Product(){Name = "Plums", StoreId = 5, Price = 3, Amount = 10},
                    new Product(){Name = "Wisteria", StoreId = 5, Price = 7, Amount = 5}

                };
                db.Products.AddRange(products);
                db.SaveChanges();

                // Поиск  товара по определенному магазину (Используя оператор LIKE in SQL).
                Console.WriteLine("----------------------------------");
                var retrievedStores = db.Stores.Where(s => EF.Functions.Like(s.Name!, "%1%"));
                foreach (Store s in retrievedStores)
                {
                    Console.WriteLine($"Products in {s.Name}");
                    foreach (Product p in s.Products)
                    {
                        Console.WriteLine($"---{p.Name}");
                    }
                }

                // Поиск товара по всем магазинам.(Используя оператор LIKE in SQL).
                Console.WriteLine("----------------------------------");
                var retrievedProducts = db.Products.Where(p => EF.Functions.Like(p.Name!, "%Ro%"));
                foreach (Product p in retrievedProducts)
                {
                    Console.WriteLine($"Product {p.Name} is in the following stores:");
                    Console.WriteLine($"---{p.Store.Name}");
                }

                // Получение случайного товара из определенного магазина.
                Console.WriteLine("----------------------------------");
                var randomProduct = db.Products
                    .Where(p => p.StoreId == 1)
                    .OrderBy(p => EF.Functions.Random()) // Random ordering
                    .FirstOrDefault(); // Get a single random product

                Console.WriteLine($"Random product from {randomProduct.Store.Name}: {randomProduct.Name}");

                //Сортировка цветов: по убыванию и возрастанию их стоимости.
                Console.WriteLine("----------------------------------");
                var flowers = db.Products
                    .OrderBy(p => p.Price) // Sort by price in ascending order
                    .ToList();
                Console.WriteLine("Flowers in ASC order by price:");
                foreach (Product p in flowers)
                {
                    Console.WriteLine($"---{p.Name} : {p.Price} CAD");
                }

                Console.WriteLine("----------------------------------");
                var flowersDesc = db.Products
                    .OrderByDescending(p => p.Price) // Sort by price in descending order
                    .ToList();
                Console.WriteLine("Flowers in DESC order by price:");
                foreach (Product p in flowersDesc)
                {
                    Console.WriteLine($"---{p.Name} : {p.Price} CAD");
                }

                // На основе анонимного типа, сформировать отчет, содержащий: общее количество товара 
                //в наличии и общую сумму товара для каждого из магазинов в определенном городе(выбор города по ID).
                Console.WriteLine("----------------------------------");
                var totalAmountInStore = db.Products
                    .Where(p => p.StoreId == 1)  // Filter by store
                    .Sum(p => p.Amount);
                Console.WriteLine($"Total amount of all products in Store 1: {totalAmountInStore}");

                var totalAmountInCity = db.Products
                    .Where(p => p.Store.City.Name == "Calgary") // Filter by city via Store
                    .Sum(p => p.Amount);
                Console.WriteLine($"Total amount of all products in Calgary: {totalAmountInCity}");

                /*
                 Вывести для владельца общую информацию о его бизнесе  виде:

-- Город
   -- Магазин 1
      -- Товары: ...
   -- Магазин 2
      -- Товары: …

 и т.д..
                 */
                Console.WriteLine("----------------------------------");
                // Fetch cities with stores and products
                var retrievedCities = db.Cities
                    .Include(c => c.Stores)
                    .ThenInclude(s => s.Products)
                    .ToList();
                // Print results in hierarchical format
                foreach (var city in retrievedCities)
                {
                    Console.WriteLine($"-{city.Name}");

                    foreach (var store in city.Stores)
                    {
                        Console.WriteLine($" --{store.Name}");

                        foreach (var product in store.Products)
                        {
                            Console.WriteLine($"  ---{product.Name}");
                        }
                    }
                }

            }
        }
    }
}
