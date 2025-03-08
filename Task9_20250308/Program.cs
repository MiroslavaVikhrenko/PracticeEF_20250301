using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Task9_20250308
{
    internal class Program
    {
        /*
         Опишите класс «Магазин» и «Игра», реализуйте связь один ко многим между таблицами.

Заполните таблицы данными, используя следующие подходы:
1) Установка главной сущности по навигационному свойству зависимой сущности.
2) Установка главной сущности по свойству-внешнему ключу зависимой сущности.
3) Установка зависимой сущности через навигационное свойство главной сущности.
         */
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //1) Установка главной сущности по навигационному свойству зависимой сущности.
                SetMainEntityByDependentEntityNavProp(db);
                //2) Установка главной сущности по свойству-внешнему ключу зависимой сущности.
                SetMainEntityByForeignKeyPropOfDependentEntity(db);
                //3) Установка зависимой сущности через навигационное свойство главной сущности.
                SetDependentEntityByNavPropOfMainEntity(db);
            }         
        }

        //3) Установка зависимой сущности через навигационное свойство главной сущности.
        public static void SetDependentEntityByNavPropOfMainEntity(ApplicationContext db)
        {
                Game game1 = new Game { Name = "Purple game" };
                Game game2 = new Game { Name = "Blue game" };
                Game game3 = new Game { Name = "Red game" };
                Shop shop1 = new Shop { Name = "Wisteria shop", Games = { game1, game2 } };
                Shop shop2 = new Shop { Name = "Camomile shop", Games = { game3 } };
                db.Shops.AddRange(shop1, shop2);  // add shops
                db.Games.AddRange(game1, game2, game3);     // add games
                db.SaveChanges();

                Console.WriteLine("-----------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Set Dependent Entity By Navigation Property Of Main Entity");
                Console.ResetColor();
                foreach (var game in db.Games.ToList())
                {
                    Console.WriteLine($"{game.Name} is in {game.Shop?.Name}");
                }
        }


        //2) Установка главной сущности по свойству-внешнему ключу зависимой сущности.
        public static void SetMainEntityByForeignKeyPropOfDependentEntity(ApplicationContext db)
        {
                Shop shop1 = new Shop { Name = "Sakura shop" };
                Shop shop2 = new Shop { Name = "Camelia shop" };
                db.Shops.AddRange(shop1, shop2);  // add shops
                db.SaveChanges();
                Game game1 = new Game { Name = "Pink game", ShopId = shop1.Id };
                Game game2 = new Game { Name = "Orange game", ShopId = shop1.Id };
                Game game3 = new Game { Name = "Green game", ShopId = shop2.Id };
                db.Games.AddRange(game1, game2, game3);     // add games
                db.SaveChanges();

                Console.WriteLine("-----------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Set Main Entity By Foreign Key Property Of Dependent Entity");
                Console.ResetColor();
                foreach (var game in db.Games.ToList())
                {
                    Console.WriteLine($"{game.Name} is in {game.Shop?.Name}");
                }
        }

        //1) Установка главной сущности по навигационному свойству зависимой сущности.
        public static void SetMainEntityByDependentEntityNavProp(ApplicationContext db)
        {
            
                Shop shop1 = new Shop { Name = "Rose shop" };
                Shop shop2 = new Shop { Name = "Violet shop" };
                Game game1 = new Game { Name = "Black game", Shop = shop1 };
                Game game2 = new Game { Name = "White game", Shop = shop2 };
                Game game3 = new Game { Name = "Gray game", Shop = shop2 };
                db.Shops.AddRange(shop1, shop2);  // add shops
                db.Games.AddRange(game1, game2, game3);     // add games
                db.SaveChanges();
                Console.WriteLine("-----------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Set Main Entity By Dependent Entity Navingation Property");
                Console.ResetColor();
                foreach (var game in db.Games.ToList())
                {
                    Console.WriteLine($"{game.Name} is in {game.Shop?.Name}");
                }
        }
    }
}
