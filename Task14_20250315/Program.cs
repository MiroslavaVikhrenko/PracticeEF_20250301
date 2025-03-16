using Microsoft.EntityFrameworkCore;

namespace Task14_20250315
{
    /*
     Создайте приложение, которое содержит таблицу «Меню». 
    Данная таблица содержит пункты меню, при этом один пункт, 
    может содержать сколько угодно подпунктов или ни одного и 
    при этом сам может иметь родительский пункт меню. Написать запрос для получения подобной иерархии:
-File
--Open
--Save
--Save As
---To hard-drive..
---To online-drive..
-Edit
-View

    рекурсия +  lazy loading
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var menuItems = new List<MenuItem>()
                {
                    new MenuItem {Name = "File"},
                    new MenuItem {Name = "Edit"},
                    new MenuItem {Name = "View"}
                };

                menuItems.Add(new MenuItem { Name = "Open", ParentItem = menuItems[0] });
                menuItems.Add(new MenuItem { Name = "Save", ParentItem = menuItems[0] });
                menuItems.Add(new MenuItem { Name = "Save As", ParentItem = menuItems[0] });
                menuItems.Add(new MenuItem { Name = "To hard-drive..", ParentItem = menuItems[5] });
                menuItems.Add(new MenuItem { Name = "To online-drive..", ParentItem = menuItems[5] });

                db.MenuItems.AddRange(menuItems);
                db.SaveChanges();

                var items = db.MenuItems.ToList();

                foreach (MenuItem item in items)
                {
                    if (item.ParentItem == null)
                    {
                        Console.WriteLine($" -{item.Name}");
                        if (item.ChildItems != null)
                        {
                            foreach (MenuItem child in item.ChildItems)
                            {
                                Console.WriteLine($" --{child.Name}");
                                if (child.ChildItems != null)
                                {
                                    foreach (MenuItem c in child.ChildItems)
                                    {
                                        Console.WriteLine($" ---{c.Name}");
                                    }
                                }

                            }
                        }

                    }
                }
            }
        }

        
    }
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual MenuItem? ParentItem { get; set; } // nav prop 
        public int? ParentItemId { get; set; }
        public virtual List<MenuItem> ChildItems { get; set; } = new();// nav prop
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_Menu;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.ParentItem)
                .WithMany(m => m.ChildItems)
                .HasForeignKey(m => m.ParentItemId); // Ensure correct foreign key

            modelBuilder.Entity<MenuItem>().Property(i => i.Name).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

