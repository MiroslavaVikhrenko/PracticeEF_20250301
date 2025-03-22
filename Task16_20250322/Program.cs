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
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //List<City> cities = new List<City>()
                //{
                //    new City {Name = "Calgary"},
                //    new City {Name = "Edmonton"},
                //    new City {Name = "Red Deer"}
                //};
                //db.Cities.AddRange(cities);
                //db.SaveChanges();

                //List<Supplier> suppliers = new List<Supplier>()
                //{
                //    new Supplier{Name = "Supplier 1"},
                //    new Supplier{Name = "Supplier 2"},
                //    new Supplier{Name = "Supplier 3"}
                //};
                //db.Suppliers.AddRange(suppliers);
                //db.SaveChanges();                

                //List<Store> stores = new List<Store>()
                //{
                //    new Store() { Name = "Store 1", CityId = 1, Address = "190-289", Suppliers = new List<Supplier>(){suppliers[0], suppliers[1] } },
                //    new Store() { Name = "Store 2", CityId = 1, Address = "241-234", Suppliers = new List<Supplier>(){suppliers[1], suppliers[2] } },
                //    new Store() { Name = "Store 3", CityId = 2, Address = "129-567", Suppliers = new List<Supplier>(){suppliers[0], suppliers[2] } },
                //    new Store() { Name = "Store 4", CityId = 3, Address = "309-222", Suppliers = new List<Supplier>(){suppliers[0], suppliers[1], suppliers[2] } },
                //    new Store() { Name = "Store 5", CityId = 3, Address = "987-123", Suppliers = new List<Supplier>(){suppliers[1] } }
                //};
                //db.Stores.AddRange(stores);
                //db.SaveChanges();

                //List<Product> products = new List<Product>()
                //{
                //    new Product(){Name = "Roses", StoreId = 1},
                //    new Product(){Name = "Lilies", StoreId = 1},
                //    new Product(){Name = "Peonies", StoreId = 2},
                //    new Product(){Name = "Azisai", StoreId = 2},
                //    new Product(){Name = "Camomiles", StoreId = 3},
                //    new Product(){Name = "Sakura", StoreId = 4},
                //    new Product(){Name = "Plums", StoreId = 5},
                //    new Product(){Name = "Wisteria", StoreId = 5}

                //};
                //db.Products.AddRange(products);
                //db.SaveChanges();

                // Поиск  товара по определенному магазину (Используя оператор LIKE in SQL).

            }
        }
    }
}
