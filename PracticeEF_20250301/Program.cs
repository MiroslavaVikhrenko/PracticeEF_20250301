using Microsoft.EntityFrameworkCore;

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
            
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }

    public class  ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=TaskMarch1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(e => new { e.Id, e.Name });
            modelBuilder.Entity<User>().Property(e => e.Position).HasMaxLength(70);
            modelBuilder.Entity<User>().ToTable(e => e.HasCheckConstraint("Age", "Age > 0 AND Age < 100"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
