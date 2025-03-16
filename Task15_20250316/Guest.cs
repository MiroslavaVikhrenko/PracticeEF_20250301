using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_20250316
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<GuestEvent> GuestEvents { get; set; } = new(); //nav prop
    }
}
