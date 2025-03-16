using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_20250316
{
    public enum Role: int
    {
        Speaker,
        Manager,
        Visitor
    }
    public class GuestEvent
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public virtual Guest? Guest { get; set; } // nav prop
        public int GuestId { get; set; } // foreign key
        public virtual Event? Event { get; set; } // nav prop
        public int EventId { get; set; } // foreign key

    }
}
