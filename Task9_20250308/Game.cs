using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Task9_20250308
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShopId { get; set; }      // foreign key
        public Shop? Shop { get; set; }    // navigation property
    }
}
