using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Task5_20250305
{
    internal class Program
    {
        /*
         Создайте приложение с возможностью авторизации. 
        При третьей подряд попытке входа с неверным логином, 
        запретите возможность авторизации для пользователя (на уровне базы данных).  

Пароль в базе данных храните в зашифрованном виде.

Расширьте данное приложение только для авторизованных пользователей:

Реализуйте возможность просмотра информации о книг.
Реализуйте возможность поиска определенной книги (используйте метод расширения Where).
Реализуйте возможность пагинации (За раз выводится по 5 книг, разместите внизу ‘текстовые кнопки’: 
        «Назад» и «Вперед», с помощью которых передвигайтесь по коллекции книг). 
Для пагинации, вам пригодятся методы расширения Skip и Take, изучите их самостоятельно.

 Для этого задания, используйте 2 сущности: «User» и «Book».
         
         */
        static void Main(string[] args)
        {
            //AddUser("tanaka", "123Abc#@");
            
            List<Book> books = new List<Book>
            {
                new Book{Title = "Snow country", Author = "Yasunari Kawabata", Description="A delicate, melancholic novel about a fleeting love affair between a wealthy Tokyo man and a rural geisha, set against the hauntingly beautiful backdrop of Japan’s snow-covered mountains."},
                new Book{Title = "Kinkakuji", Author = "Yukio Mishima", Description = "A psychological novel exploring obsession, beauty, and destruction through the story of a troubled young monk who becomes fixated on the famed Kinkaku-ji temple, ultimately leading him to commit an act of arson."},
                new Book{Title = "Norwegian Wood", Author = "Haruki Murakami", Description="A nostalgic coming-of-age novel about love, loss, and the emotional struggles of a university student in 1960s Japan."},
                new Book{Title="The Sound of the Mountain", Author="Yasunari Kawabata", Description="A poignant exploration of aging, family relationships, and existential loneliness through the eyes of an elderly man in post-war Japan."},
                new Book{Title="The Sailor Who Fell from Grace with the Sea", Author="Yukio Mishima", Description="A dark and intense novel about a young boy’s descent into nihilism as he idolizes and then betrays his mother’s new lover, a sailor."},
                new Book{Title="The Woman in the Dunes", Author="Kobo Abe", Description="A surreal and suspenseful novel about a man trapped in a sand dune with a mysterious woman, symbolizing existential struggle and human resilience."},
                new Book{Title="Convenience Store Woman", Author="Sayaka Murata", Description="A sharp, satirical novel about a socially unconventional woman who finds purpose working in a convenience store, challenging societal expectations of normalcy."},
                new Book{Title="Out", Author="Natsuo Kirino", Description="A gripping crime thriller following four women who become entangled in murder, covering themes of gender roles, violence, and survival in modern Japan."},
                new Book{Title="I Am a Cat", Author="Natsume Soseki", Description="A humorous and philosophical novel narrated by a sardonic cat who observes and critiques the absurdities of human society in Meiji-era Japan."},
                new Book{Title="Kafka on the Shore", Author="Haruki Murakami", Description="A dreamlike and enigmatic novel intertwining the journeys of a runaway teenage boy and an elderly man who can talk to cats, blending reality and metaphysical elements."}
            };
            //AddBooks(books);


            if (DisplayLoginMenu())
            {
                DisplayBooksMenu();
            }

        }

        public static void SearchForACertainBook()
        {
            Console.Clear();
            Console.WriteLine("enter a title of book you are looking for:");
            string search = Console.ReadLine();

            using (ApplicationContext db = new ApplicationContext())
            {

            }

        }
        public static void ShowAllBooks()
        {
            int pageSize = 5; // Number of books per page
            int currentPage = 1; // Start at page 1

            using (ApplicationContext db = new ApplicationContext())
            {
                int totalBooks = db.Books.Count();
                int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

                while (true)
                {
                    Console.Clear(); // Clear console for better readability
                    Console.WriteLine($"Page {currentPage} of {totalPages}");

                    var books = db.Books
                        .OrderBy(b => b.Id) // Ensure consistent ordering
                        .Skip((currentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.ToString()}\n");
                    }

                    Console.WriteLine("\nOptions:");
                    if (currentPage > 1) Console.WriteLine("Type 'prev' to go to the previous page.");
                    if (currentPage < totalPages) Console.WriteLine("Type 'next' to go to the next page.");
                    Console.WriteLine("Type 'exit' to close the program.");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "next" && currentPage < totalPages)
                    {
                        currentPage++;
                    }
                    else if (choice == "prev" && currentPage > 1)
                    {
                        currentPage--;
                    }
                    else if (choice == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Press any key to try again...");
                        Console.ReadKey();
                    }
                }
            }

        }
        public static void DisplayBooksMenu()
        {
            Console.Clear();
            Console.WriteLine("\nBook Menu:");
            Console.WriteLine("1. Show All Books");
            Console.WriteLine("2. Search for a Certain Book");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowAllBooks();
                    break;
                case "2":
                    SearchForACertainBook();
                    break;
                case "3":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
        public static bool DisplayLoginMenu()
        {
            int count = 0;
            while (count < 3)
            {
                Console.Clear();
                Console.WriteLine("Please enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password:");
                string password = Console.ReadLine();

                if (!Login(username, password))
                {
                    count++;
                    if (count < 3)
                    {
                        Console.WriteLine("Try again");
                        Console.ReadKey();
                        if (count == 2)
                        {
                            Console.WriteLine("You have the last attempt, be careful!");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was the last attempt!");
                    }
                    
                }
                else
                {
                    return true;
                }
            }
            if (count >= 3)
            {
                Console.WriteLine("You entered wrong credentials 3 times, you are blocked!");
                Console.ReadKey();
                return false;
            }
            return false;
        }

        public static bool Login(string username, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    string salt = user.Salt;
                    string hashedPassword2 = SecurityHelper.HashPassword(password, salt, 10101, 70);
                    if (hashedPassword2 == user.Hashed_Password)
                    {
                        Console.WriteLine($"User {user.UserName} successfully logged in");
                        Console.ReadKey();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect credentials");
                        Console.ReadKey();
                        return false;
                    }
                }
            }
            return false;
        }

        public static void AddBooks(List<Book> books)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Books.AddRange(books);
                db.SaveChanges();
            }
        }

        public static void AddUser(string name, string password)
        {
            string salt = SecurityHelper.GenerateSalt(70);
            //Console.WriteLine(salt);
            string hashedPassword = SecurityHelper.HashPassword(password, salt, 10101, 70);
            //Console.WriteLine(hashedPassword);
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(new User { UserName = name, Salt = salt, Hashed_Password = hashedPassword });
                db.SaveChanges();
            }
        }
    }

}
