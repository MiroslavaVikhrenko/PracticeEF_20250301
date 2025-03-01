using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PracticeEF_20250301
{
    /*
     Опишите таблицу (на любую тему). В таблице через Fluent Api или аннотации, примените следующие ограничения: 

Составной ключ на 2 столбца.
Ограничение длины для строкового столбца.
Атрибут CHECK для возраста.
Атрибут CHECK для должности работника (или другое с использование перечисления (Enum)).
Указание другого имени ключа для таблицы (В классе использовать Id, для таблицы к примеру - ProductId).
Использовать обязательные и не обязательные свойства (с атрибутом Required).
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = new List<User>
                {
                    new User{Name = "Tanaka", Age = 30, Position = Position.Manager},
                    new User{Name = "Suzuki", Age = 35, Position = Position.Designer}
                };
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public Position Position { get; set; }
    }
    public enum Position : int
    {
        Manager,
        Designer
    }
    public class  ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=TaskMarch1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(e => new { e.Id, e.Name });
            modelBuilder.Entity<User>().Property(e => e.Position).HasMaxLength(70);
            modelBuilder.Entity<User>().ToTable(e => e.HasCheckConstraint("Age", "Age > 0 AND Age < 100"));
            modelBuilder.Entity<User>().ToTable(e => e.HasCheckConstraint("Position", $"Position in ({(int)Position.Manager},{(int)Position.Designer})"));
            modelBuilder.Entity<User>().Property(e => e.Id).HasColumnName("UserId");
            base.OnModelCreating(modelBuilder);
        }
    }
}
