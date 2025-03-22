using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task16_20250322
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Store> Stores { get; set; } = new(); //nav prop
    }
}
