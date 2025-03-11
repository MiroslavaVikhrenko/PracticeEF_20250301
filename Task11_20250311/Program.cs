using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task11_20250311
{
    /*
     Реализовать 2 класса: «Пользователь» и «Настройки пользователя». 
    Организовать между таблицами связь один к одному. 
    Добавить несколько пользователей и их настройки. 
    Достать пользователя с Id = 2 и его настройки. Удалить пользователя с Id 3 
    (автоматически должен удалится профайл пользователя).
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //User u1 = new User() { Name = "Tanaka" };
                //User u2 = new User() { Name = "Yamada" };
                //User u3 = new User() { Name = "Hayashi" };
                //User u4 = new User() { Name = "Okuda" };
                //User u5 = new User() { Name = "Nishida" };

                //UserSettings us1 = new UserSettings() { Name = "Tokyo", User = u1 };
                //UserSettings us2 = new UserSettings() { Name = "Osaka", User = u2 };
                //UserSettings us3 = new UserSettings() { Name = "Nagoya", User = u3 };
                //UserSettings us4 = new UserSettings() { Name = "Sapporo", User = u4 };
                //UserSettings us5 = new UserSettings() { Name = "Kyoto", User = u5 };

                //db.UserSettings.AddRange(us1, us2, us3, us4, us5);
                //db.Users.AddRange(u1, u2, u3, u4, u5);
                //db.SaveChanges();

                // Достать пользователя с Id = 2 и его настройки.

                // Explicit loading (явная загрузка)

                User? user = db.Users.FirstOrDefault(u => u.Id == 2);
                if (user != null)
                {
                    db.UserSettings.Where(us => us.User == user).Load();
                    Console.WriteLine($"User: {user.Name}");
                    Console.WriteLine($"User Settings: {user.UserSettings.Name}");
                }

                ////Удалить пользователя с Id 3
                ////(автоматически должен удалится профайл пользователя).
                User? user2 = db.Users.FirstOrDefault(u => u.Id == 3);
                if (user2 != null)
                {
                    db.UserSettings.Where(us => us.User == user2).Load();
                    db.Users.Remove(user2);
                    db.SaveChanges();
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property (1-to-1)
        public UserSettings? UserSettings { get; set; }
    }

    public class UserSettings
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }  // Foreign Key
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
