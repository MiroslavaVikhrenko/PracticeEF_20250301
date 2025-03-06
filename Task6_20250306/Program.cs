using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Task6_20250306
{
    internal class Program
    {
        /*
         У вас есть приложение для управления учебными курсами в университете. 
        Вам необходимо создать модель данных для таблицы «Студенты». У студента должны быть следующие свойства: 

Идентификатор (Id) 
Имя (Name) 
Фамилия (LastName) 
Электронная почта (Email) 
Дата рождения (DateOfBirth) 
Год поступления (YearOfAdmission) 
Группа (Group)

Используя Fluent API в EF Core, создайте модель данных для таблицы "Студенты" согласно следующим требованиям: 
Имя – обязательное, минимальная длина 5 символов.
Фамилия – обязательная, должна начинаться не с буквы ‘К’.
Электронная почта – не обязательная, но если добавили, проверять на валидность (используя regex) 
Дата рождения - не обязательная, со значением по умолчанию – 1900.
Год поступления – генерируется автоматически, при добавлении записи в таблицу.
Группа – уникальный GUID идентификатор, но в классе определенно как тип данных string.

         */
        static void Main(string[] args)
        {
            
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //[RegularExpression(@"", ErrorMessage ="invalid email format")]
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime YearOfAdmission { get; set; }
        public string Group {  get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
            Database.EnsureDeleted();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=Task_0306;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Student>().ToTable(s => s.HasCheckConstraint("Name", "LEN(Name) > 5"));
            modelBuilder.Entity<Student>().Property(s => s.LastName).IsRequired();
            modelBuilder.Entity<Student>().ToTable(s => s.HasCheckConstraint("LastName", "LastName NOT LIKE 'S%'"));
            //Электронная почта – не обязательная, но если добавили, проверять на валидность(используя regex)
            //modelBuilder.Entity<Student>().HasCheckConstraint("CK_User_Email", "Email LIKE '%@%.%'");
            modelBuilder.Entity<Student>().Property(s => s.DateOfBirth).HasDefaultValue("1900");
            modelBuilder.Entity<Student>().Property(s => s.YearOfAdmission).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Student>().Property(s => s.Group).HasDefaultValueSql("NEWID()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
