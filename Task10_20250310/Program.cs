namespace Task10_20250310
{
    /*
     Опишите 3 связанных таблицы: «Компания»,
«Магазин» и «Покупатели». Каждый магазин должен принадлежать к
определенной компании, каждый покупатель может принадлежать к нескольким
магазинам. 
 
Реализуйте возможность добавления и вывода информации, к примеру: 
 
1) Отобразите информацию обо всех компаниях.
 
Название компании, список магазинов (магазинов со своими покупателями может быть несколько), список покупателей для данного магазина.
 
2) Отобразите информацию о покупателях.
 
Фио пользователя, список магазинов и их компаний.
     */
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shop> Shops { get; set; } // nav prop
    }
    public class Shop
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int CompanyId { get; set; } // foreign key
        public Company Company { get; set; } //nav prop
        public List<Customer> Customers { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shop> Shops { get; set; } //nav prop
    }
}
