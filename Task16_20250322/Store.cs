using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task16_20250322
{
    /*
     Каждый цветочный магазин расположен в определенном городе и имеет адрес. 
    У каждого цветочного магазина может быть один или несколько поставщиков и 
    коллекция товаров (цветы, упаковочные коробки и т.д.). 
     */
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual City City { get; set; } // nav prop
        public int CityId { get; set; } // foreign key
        public virtual List<Supplier> Suppliers { get; set; } = new(); // nav prop
        public virtual List<Product> Products { get; set; } = new(); // nav prop
    }
}
