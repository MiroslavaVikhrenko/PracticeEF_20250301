using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22_20250329
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public Client Client { get; set; }
    }
}
