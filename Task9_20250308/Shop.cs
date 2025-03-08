using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_20250308
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; } = new();
    }
}
