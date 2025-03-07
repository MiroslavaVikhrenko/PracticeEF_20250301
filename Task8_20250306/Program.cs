namespace Task8_20250306
{
    internal class Program
    {
        /*
         Разработайте приложения для учета продуктов в интернет-магазине. 
        Вам необходимо создать модель данных для таблицы «Продукты». У продукта должны быть следующие свойства: 

Идентификатор (Id) 
Название (Name) 
Цена (Price) 
Категория (Category) 
Описание (Description) 

Ваша задача состоит в следующем. Используя Fluent API в EF Core, 
        создайте модель данных для таблицы «Продукты» согласно требованиям ниже:

Убедитесь, что свойства имеют соответствующие типы данных в базе данных. 
Для принятия данных, определите конструктор.
Используйте составной ключ.

Установите ограничение, чтобы название продукта (Name) было обязательным и не превышало 100 символов. 

Установите ограничение, чтобы цена продукта (Price) была больше 0. 

Вместо одного свойства, используйте поле.

При необходимости выполните другие дополнительные настройки модели данных, 
        которые могут потребоваться для вашего приложения учета продуктов.
         */
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                List<Product> products = new List<Product>
                {
                    new Product("table", "japanese table", 100.54m, "furniture"),
                    new Product("chair", "chinese table", 80m, "furniture"),
                    new Product("sofa", "korean table", 199.99m, "furniture"),
                    new Product("candies", "wagashi", 10.50m, "food"),
                    new Product("chips", "canadian snacks", 5.5m, "food"),
                    new Product("chocolate", "belgian chocolate", 15.3m, "food"),
                    new Product("dress", "french dress", 50m, "clothes"),
                    new Product("jeans", "american jeans", 38.41m, "clothes"),
                    new Product("jacket", "norwegian jacket", 100m, "clothes"),
                    new Product("coat", "ukrainian coat", 200m, "clothes"),
                };
                db.Products.AddRange(products);
                db.SaveChanges();

            }
        }
    }
}
