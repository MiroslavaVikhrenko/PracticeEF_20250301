using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task25_20250401
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } // foreign key
        public Customer Customer { get; set; } // nav prop
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
