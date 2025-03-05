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
            AddUser("tanaka", "123Abc#@");
            
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
            AddBooks(books);
            
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
