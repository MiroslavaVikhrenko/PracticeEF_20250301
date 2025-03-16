using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_20250316
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Guest>? Guests { get; set; } = new(); // nav prop
    }
}
