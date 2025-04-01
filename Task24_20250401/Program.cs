using Microsoft.EntityFrameworkCore;

namespace Task24_20250401
{
    /*
     У вас есть книжный магазин, который продает книги различных авторов. 
    Добавьте возможность быстро изменять цены на книги авторов. 
    Для этого вы используйте хранимую процедуру в базе данных.

    Создайте хранимую процедуру, которая позволит вам обновлять цены на книги определенного автора. 
    Используйте Entity Framework Core для вызова этой хранимой процедуры из кода вашего приложения.
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

                int authorId = 1; 
                decimal priceIncrease = 5.00m;

                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                Console.WriteLine($"Increasing book prices for {author.Name} (Id = {authorId}) by {priceIncrease}...");
                UpdateBookPrices(authorId, priceIncrease, db);

                var books = db.Books.Where(b => b.AuthorId == authorId).ToList();
                Console.WriteLine("Updated Book Prices:");
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title}: ${book.Price}");
                }
            }
        }

        // Execute Stored Procedure in C#
        public static void UpdateBookPrices(int authorId, decimal priceIncrease, ApplicationContext db)
        {
            db.Database.ExecuteSqlRaw("EXEC dbo.UpdateBookPricesByAuthor @p0, @p1", authorId, priceIncrease);
        }

        // Create Stored Procedure from C#
        public static void CreateStoredProcedure(ApplicationContext db)
        {
            var sql = @"
    IF OBJECT_ID('dbo.UpdateBookPricesByAuthor', 'P') IS NULL
    EXEC('
    CREATE PROCEDURE dbo.UpdateBookPricesByAuthor
        @AuthorId INT,
        @PriceIncrease DECIMAL(18,2)
    AS
    BEGIN
        UPDATE Books
        SET Price = Price + @PriceIncrease
        WHERE AuthorId = @AuthorId;
    END;')";

            db.Database.ExecuteSqlRaw(sql);
        }

        // Seed data to db
        public static void SeedDatabase(ApplicationContext db)
        {
            db.Authors.AddRange(new List<Author>
            {
                new() { Name = "Haruki Murakami" },
                new() { Name = "Yukio Mishima" },
                new() { Name = "Natsume Soseki" },
                new() { Name = "Ryunosuke Akutagawa" },
                new() { Name = "Banana Yoshimoto" }
            });
            db.SaveChanges();

            db.Books.AddRange(new List<Book>
            {
             // Haruki Murakami
                new() { Title = "Norwegian Wood", Price = 18.99m, AuthorId = 1 },
                new() { Title = "Kafka on the Shore", Price = 20.99m, AuthorId = 1 },
                new() { Title = "1Q84", Price = 25.99m, AuthorId = 1 },

            // Yukio Mishima
                new() { Title = "The Temple of the Golden Pavilion", Price = 16.99m, AuthorId = 2 },
                new() { Title = "Confessions of a Mask", Price = 14.99m, AuthorId = 2 },
                new() { Title = "Spring Snow", Price = 17.99m, AuthorId = 2 },

            // Natsume Soseki
                new() { Title = "Kokoro", Price = 15.99m, AuthorId = 3 },
                new() { Title = "Botchan", Price = 13.99m, AuthorId = 3 },
                new() { Title = "I Am a Cat", Price = 19.99m, AuthorId = 3 },

            // Ryunosuke Akutagawa
                new() { Title = "Rashomon and Other Stories", Price = 11.99m, AuthorId = 4 },
                new() { Title = "Hell Screen", Price = 13.49m, AuthorId = 4 },
                new() { Title = "Kappa", Price = 12.99m, AuthorId = 4 },

            // Banana Yoshimoto
                new() { Title = "Kitchen", Price = 14.99m, AuthorId = 5 },
                new() { Title = "Goodbye Tsugumi", Price = 13.49m, AuthorId = 5 },
                new() { Title = "Asleep", Price = 15.99m, AuthorId = 5 }
            });
            db.SaveChanges();
        }
    }
}
