using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Task4_20250303
{
    internal class Program
    {
        /*
         Создайте приложение консольного типа с возможностью авторизации и регистрации пользователя. 
        После авторизации или регистрации, перенаправляйте пользователя на главное меню приложения. 
        Пароль в базе данных храните в зашифрованном виде.
         */
        static void Main(string[] args)
        {
            string pwd = "123Abc#@";
            string salt = SecurityHelper.GenerateSalt(70);
            //Console.WriteLine(salt);
            string hashedPassword = SecurityHelper.HashPassword(pwd, salt, 10101, 70);
            //Console.WriteLine(hashedPassword);

            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    db.Users.Add(new User { UserName = "tanaka", Salt = salt, Hashed_Password = hashedPassword });
            //    db.SaveChanges();
            //}

            string pwd2 = "123Abc#@";
            string userName = "tanaka";

            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    string salt2 = user.Salt;
                    string hashedPassword2 = SecurityHelper.HashPassword(pwd2, salt2, 10101, 70);
                    //string hashedPassword2 = "onHIGRV76O6GjgN0X8uh53W7YNu3aPvFhbnMd5ZIrSS0umUBK9l055+UimDBzbFs50hMHnrKZ+0txa4Fb2T7cdAWA==";
                    if (hashedPassword2 == user.Hashed_Password)
                    {
                        Console.WriteLine("Passed");
                    }
                    else
                    {
                        Console.WriteLine("Did not pass");
                    }
                }
                
            }

        }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Hashed_Password { get; set; }
        public string Salt { get; set; }

    }

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=BJH;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(t => t.Hashed_Password).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(t => t.UserName).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(t => t.Salt).HasMaxLength(1024);
            modelBuilder.Entity<User>().HasIndex(t => t.UserName).IsUnique();
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Hashed_Password).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Salt).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
    class SecurityHelper
    {
        public static string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public static string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }
    }


}
