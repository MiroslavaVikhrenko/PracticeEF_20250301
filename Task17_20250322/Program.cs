namespace Task17_20250322
{
    internal class Program
    {
        /*
         Создайте базу данных для управления книгами, авторами и жанрами книг. 
        Определите 3 класса: «Book», «Author, «Genre». 

Класс «Book», выглядит следующим образом:

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public Author Author { get; set; }
    public Genre Genre { get; set; }
}

Настройте связи между классами, используя Fluent Api. 
Заполните их начальными данными с использованием методов AddRange() - для добавления элементов 
        и Any() – для проверки наличия.
 
 Выполните следующие 10 запросов LINQ to Entities:

 1) Получить количество книг определенного жанра.
 2) Получить минимальную цену для книг определенного автора.
 3) Получить среднюю цену книг в определенном жанре.
 4) Получить суммарную стоимость всех книг определенного автора.
 5) Выполнить группировку книг по жанрам.
 6) Выбрать только названия книг определенного жанра.
 7) Выбрать все книги, кроме тех, что относятся к определенному жанру, используя метод Except.
 8) Объединить книги от двух авторов, используя метод Union. 
9) Достать 5-ть самых дорогих книг.
 10) Пропустить первые 10 книг и взять следующие 5.
         */
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Genre> genres = new List<Genre>()
                {
                    new Genre(){Name = "Mystery"},
                    new Genre(){Name = "Literary Fiction"},
                    new Genre(){Name = "Historical Fiction"}
                };
                db.Genres.AddRange(genres);
                db.SaveChanges();

                List<Author> authors = new List<Author>()
                {
                    new Author(){Name = "Keigo Higashino"},
                    new Author(){Name = "Soji Shimada"},
                    new Author(){Name = "Seicho Matsumoto"},
                    new Author(){Name = "Haruki Murakami"},
                    new Author(){Name = "Kenzaburo Oe"},
                    new Author(){Name = "Shusaku Endo"},
                    new Author(){Name = "Kazuo Ishiguro"}
                };
                db.Authors.AddRange(authors);
                db.SaveChanges();

                List<Book> books = new List<Book>()
                {
                    new Book(){Title = "The Devotion of Suspect X", Genre = genres[0], Author = authors[0], Price = 20},
                    new Book(){Title = "Malice", Genre = genres[0], Author = authors[0], Price = 45},
                    new Book(){Title = "The Tokyo Zodiac Murders", Genre = genres[0], Author = authors[1], Price = 30},
                    new Book(){Title = "Murder in the Crooked House", Genre = genres[0], Author = authors[1], Price = 40},
                    new Book(){Title = "Inspector Imanishi Investigates", Genre = genres[0], Author = authors[2], Price = 10},
                    new Book(){Title = "Norwegian Wood", Genre = genres[1], Author = authors[3], Price = 25},
                    new Book(){Title = "Kafka on the Shore", Genre = genres[1], Author = authors[3], Price = 50},
                    new Book(){Title = "The Silent Cry", Genre = genres[1], Author = authors[4], Price = 35},
                    new Book(){Title = "A Personal Matter", Genre = genres[1], Author = authors[4], Price = 15},
                    new Book(){Title = "The Sea and Poison", Genre = genres[1], Author = authors[5], Price = 60},
                    new Book(){Title = "Silence", Genre = genres[2], Author = authors[5], Price = 20},
                    new Book(){Title = "The Samurai", Genre = genres[2], Author = authors[5], Price = 35},
                    new Book(){Title = "The Honjin Murders", Genre = genres[2], Author = authors[2], Price = 30},
                    new Book(){Title = "An Artist of the Floating World", Genre = genres[2], Author = authors[6], Price = 20},
                    new Book(){Title = "The Buried Giant", Genre = genres[2], Author = authors[6], Price = 50}
                };
                db.Books.AddRange(books);
                db.SaveChanges();

                // 1) Получить количество книг определенного жанра.
                GetBooksByGenre(db, "Mystery");
                GetBooksByGenre(db, "Literary Fiction");
                GetBooksByGenre(db, "Historical Fiction");

                // 2) Получить минимальную цену для книг определенного автора.
                GetCheapestBookByAuthor(db, "Shusaku Endo");

                // 3) Получить среднюю цену книг в определенном жанре.
                GetAveragePriceByGenre(db, "Mystery");

                // 4) Получить суммарную стоимость всех книг определенного автора.
                GetTotalPriceByAuthor(db, "Soji Shimada");

                // 5) Выполнить группировку книг по жанрам.
                PrintBooksGroupedByGenre(db);

                // 6) Выбрать только названия книг определенного жанра.
                PrintBookTitlesByGenre(db, "Historical Fiction");
            }
        }

        // 6) Выбрать только названия книг определенного жанра.
        public static void PrintBookTitlesByGenre(ApplicationContext db, string genreName)
        {
            var bookTitles = db.Books
                .Where(b => b.Genre.Name == genreName) // Filter books by genre name
                .Select(b => b.Title) // Select only the title
                .ToList(); // Execute query in database

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"\nBooks in the '{genreName}' genre:\n");
            foreach (var title in bookTitles)
            {
                Console.WriteLine($" >>> {title}");
            }
        }

        // 5) Выполнить группировку книг по жанрам.
        public static void PrintBooksGroupedByGenre(ApplicationContext db)
        {
            var groupedBooks = db.Books
                .GroupBy(b => b.Genre.Name) // Group books by genre name
                .Select(g => new
                {
                    Genre = g.Key, // Genre name
                    Books = g.Select(b => new { b.Title, b.Author.Name, b.Price }) // Select relevant book details
                })
                .ToList(); // Execute in database
            Console.WriteLine("-------------------------------");
            Console.WriteLine("\nBooks grouped by genres\n");
            // Print grouped books
            foreach (var group in groupedBooks)
            {
                Console.WriteLine($"Genre: {group.Genre}");
                foreach (var book in group.Books)
                {
                    Console.WriteLine($" >>> {book.Title} by {book.Name}, Price: ${book.Price:F2}");
                }
                Console.WriteLine(); 
            }
        }

        // 4) Получить суммарную стоимость всех книг определенного автора.
        public static void GetTotalPriceByAuthor(ApplicationContext db, string authorName)
        {
            decimal? totalPrice = db.Books
                .Where(b => b.Author.Name == authorName) // Filter books by author name
                .Select(b => b.Price) // Select only the price
                .Sum(); // Get the total sum

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"\nTotal price for all books by {authorName} is ${totalPrice:F2}\n");
        }

        // 3) Получить среднюю цену книг в определенном жанре.
        public static void GetAveragePriceByGenre(ApplicationContext db, string genreName)
        {
            decimal? averagePrice = db.Books
                .Where(b => b.Genre.Name == genreName)
                .Select(b => b.Price)
                .AsEnumerable() // Fetch data into memory before applying DefaultIfEmpty
                .DefaultIfEmpty(0) // Prevents exception if no books exist
                .Average();

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"\nAverage price for books in {genreName} genre is ${averagePrice:F2}\n");
        }
        // 2) Получить минимальную цену для книг определенного автора.
        public static void GetCheapestBookByAuthor(ApplicationContext db, string authorName)
        {
            Book? cheapestBook = db.Books
                .Where(b => b.Author.Name == authorName)
                .OrderBy(b => b.Price) // Sort books by price (ascending)
                .FirstOrDefault(); // Get the first book (cheapest) or null if no books exist

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"The cheapest book by {authorName}:\n");
            if (cheapestBook != null)
            {
                Console.WriteLine($" >>> {cheapestBook.Title} by {cheapestBook.Author.Name}, Price: ${cheapestBook.Price:F2}");
            }
        }
        // 1) Получить количество книг определенного жанра.
        public static void GetBooksByGenre(ApplicationContext db, string genreName)
        {
            var booksByGenre = db.Books
            .Where(b => b.Genre.Name == genreName)
            .ToList();
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Books in {genreName} genre:\n");
            if (booksByGenre.Count > 0)
            {
                foreach (var book in booksByGenre)
                {
                    Console.WriteLine($" >>> {book.Title} by {book.Author.Name}, Price: ${book.Price:F2}");
                }
            }
            else
            {
                Console.WriteLine($"No books in {genreName} found");
            }
        }
    }
}
