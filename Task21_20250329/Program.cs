namespace Task21_20250329
{
    /*
     Используя таблицы «Users» и «Companies», создать 3 хранимых процедуры:
Получение связанных данных о Users и Companies.
Используя входной параметр получить пользователей с именем наподобие “Tom”.
Используя выходной параметр, получить средний возраст по всей таблицы пользователей.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Company> companies = new List<Company>()
                {
                    new Company(){Name = "Google"},
                    new Company(){Name = "Microsoft"},
                    new Company(){Name = "Bell"},
                    new Company(){Name = "Telus"}
                };
                db.Companies.AddRange(companies);
                db.SaveChanges();

                List<User> users = new List<User>()
                {
                    new User() { Name = "Tanaka", Age = 35, CompanyId = 1 },
                    new User() { Name = "Yamada", Age = 45, CompanyId = 1 },
                    new User() { Name = "Hashimoto", Age = 50, CompanyId = 1 },
                    new User() { Name = "Ishida", Age = 60, CompanyId = 2 },
                    new User() { Name = "Takeda", Age = 23, CompanyId = 2 },
                    new User() { Name = "Sato", Age = 31, CompanyId = 2 },
                    new User() { Name = "Kobayashi", Age = 47, CompanyId = 3 },
                    new User() { Name = "Higashiyama", Age = 68, CompanyId = 3 },
                    new User() { Name = "Sasayama", Age = 29, CompanyId = 4 },
                    new User() { Name = "Okuda", Age = 49, CompanyId = 4 },
                    new User() { Name = "Ishizuka", Age = 56, CompanyId = 4 }
                };

                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
    }
}
