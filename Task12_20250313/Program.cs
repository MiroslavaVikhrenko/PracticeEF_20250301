namespace Task12_20250313
{
    /*
     Создайте базу данных, представляющую информацию о «Компаниях», их «Сотрудниках» и «Проектах». 
    Необходимо создать запрос с использованием Entity Framework Core для получения списка проектов, 
    в которых участвуют сотрудники из определенной компании. Создать два типа связи: 
    компания – сотрудники (один ко многим), 
    сотрудники – проекты (многие ко многим).
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee e = new Employee() { Name = "tom" };
                Employee e2 = new Employee() { Name = "sophia" };
                Employee e3 = new Employee() { Name = "adam" };
                Employee e4 = new Employee() { Name = "lisa" };

                Company c = new Company() { Name = "Google", Employees = new List<Employee>() { e, e2 } };
                Company c2 = new Company() { Name = "Microsoft", Employees = new List<Employee>() { e3, e4 } };

                Project p = new Project() { Name = "P1", Employees = new List<Employee>() { e, e2 } };
                Project p2 = new Project() { Name = "P2", Employees = new List<Employee>() { e, e3, e4 } };
                Project p3 = new Project() { Name = "P3", Employees = new List<Employee>() { e2, e4 } };
                Project p4 = new Project() { Name = "P4", Employees = new List<Employee>() { e3, e4 } };
                Project p5 = new Project() { Name = "P5", Employees = new List<Employee>() { e, e2, e4 } };
                db.Employees.AddRange(e, e2, e3, e4);
                db.Projects.AddRange(p, p2, p3, p4, p5);
                db.Companies.AddRange(c, c2);
                db.SaveChanges();
            }
        }   
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; } // foreign key
        public Company Company { get; set; } // nav prop
        public List<Project> Projects { get; set; } // nav prop
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } // nav prop
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } // nav prop
    }
}
