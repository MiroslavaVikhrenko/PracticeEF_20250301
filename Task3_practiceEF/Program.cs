using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Task3_practiceEF
{
    internal class Program
    {
        /*
         Разработайте систему управления задачами для команды разработки программного обеспечения. 
        Система должна позволять создавать, управлять и отслеживать выполнением задач.

 Создайте таблицу «Tasks» для хранения информации о задачах.
 
 Используя Fluent Api, настройте вашу таблицу, следующим образом:

 1) Укажите ограничения на длину текстовых полей (название, описание).
 2) Укажите ограничения на статус задачи (используйте Enum).
 3) Добавьте индекс на столбец с датой создания задачи для оптимизации запросов.
 4) Укажите ограничения и уникальные индексы для таблицы задач для обеспечения целостности данных.
 5) Добавьте проверку, чтобы дата дедлайна задачи была больше или равна дате создания задачи.
 6) Убедитесь, что название задачи уникально в пределах таблицы Tasks, чтобы не было двух задач 
        с одинаковым названием.
 
 Перед созданием базы данных, снабдите ее начальными данными, используя метод «HasData» Fluent Api. 

 Проверьте корректности работы вашей программы.
         */
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "Backend", Description = "Develop BE", Deadline = new DateTime(2025, 6, 6), Status = Status.New},
                new Task{Name = "Database", Description = "Connect DB", Deadline = new DateTime(2025, 5, 7), Status = Status.New},
                new Task{Name = "Frontend", Description = "Create FE", Deadline = new DateTime(2025, 6, 6), Status = Status.New},
                new Task{Name = "Chat bot", Description = "Develop chatbot", Deadline = new DateTime(2025, 4, 20), Status = Status.New},
                new Task{Name = "Test app", Description = "Run variuos tests", Deadline = new DateTime(2025, 8, 1), Status = Status.New}
            };
            AddTask(tasks);
            ChangeStatus(1, Status.InProgress);
            ChangeStatus(2, Status.InProgress);
            ChangeStatus(1, Status.Complete);
            FindAllTasksInCertainStatus(Status.New);
            FindAllTasksInCertainStatus(Status.InProgress);
            FindAllTasksInCertainStatus(Status.Complete);
        }

        static void FindAllTasksInCertainStatus(Status status)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.WriteLine($"Tasks in {status} status:");
                var tasks = db.Tasks.ToList();
                foreach (Task task in tasks)
                {
                    if (task.Status == status)
                    {
                        Console.WriteLine("-----------");
                        Console.WriteLine(task.ToString());
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        static void PrintAllTasks()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.WriteLine($"ALL TASKS:");
                var tasks = db.Tasks.ToList();
                foreach (Task task in tasks)
                {
                    if (task.Status == Status.New)
                    {
                        Console.WriteLine("-----------");
                        Console.ForegroundColor = ConsoleColor.Red;                        
                        Console.WriteLine(task.ToString());
                        Console.ResetColor();
                    }
                    else if (task.Status == Status.InProgress)
                    {
                        Console.WriteLine("-----------");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(task.ToString());
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("-----------");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(task.ToString());
                        Console.ResetColor();
                    }
                }
            }
        }
        static void ChangeStatus(int id, Status status)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var task = db.Tasks.FirstOrDefault(e => e.Id == id);
                if (task != null)
                {
                    task.Status = status;
                    db.SaveChanges();
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAFTER UPDATE:");
                Console.ResetColor();
                PrintAllTasks();
            }
        }
        static void AddTask(List<Task> tasks)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Tasks.AddRange(tasks);
                db.SaveChanges();
            }
        }
    }
    //Добавьте индекс на столбец с датой создания задачи для оптимизации запросов.
    //[Index("CreatedAt")]
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public override string ToString()
        {
            return $"{Id}.{Name}: {Description} | Created at {CreatedAt.Date}, deadline: {Deadline}, Status: {Status}";
        }
    }

    public enum Status : int
    {
        New,
        InProgress,
        Complete
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=TaskManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Укажите ограничения на длину текстовых полей (название, описание).
            modelBuilder.Entity<Task>().Property(t => t.Name).HasMaxLength(80);
            modelBuilder.Entity<Task>().Property(t => t.Description).HasMaxLength(100);
            //Укажите ограничения на статус задачи (используйте Enum).
            modelBuilder.Entity<Task>().ToTable(t => t.HasCheckConstraint("Status", $"Status in ({(int)Status.Complete},{(int)Status.InProgress},{(int)Status.New})"));
            //Добавьте индекс на столбец с датой создания задачи для оптимизации запросов.
            modelBuilder.Entity<Task>().HasIndex(t => t.CreatedAt);
            //Укажите ограничения и уникальные индексы для таблицы задач для обеспечения целостности данных.
            modelBuilder.Entity<Task>().HasIndex(t => t.Description).IsUnique();
            //Добавьте проверку, чтобы дата дедлайна задачи была больше или равна дате создания задачи.
            modelBuilder.Entity<Task>().ToTable(t => t.HasCheckConstraint("Deadline", "Deadline >= CreatedAt"));
            //Убедитесь, что название задачи уникально в пределах таблицы Tasks, чтобы не было двух задач 
            //с одинаковым названием.
            modelBuilder.Entity<Task>().HasIndex(t => t.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
