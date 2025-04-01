using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task23_20250331
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public bool IsCanceled { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new(); // nav prop
    }
}
