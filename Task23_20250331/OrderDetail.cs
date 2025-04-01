using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task23_20250331
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // foreign key
        public Order Order { get; set; } // nav prop
        public int ProductId { get; set; } // foreign key
        public Product Product { get; set; } // nav prop
        public int Quantity { get; set; }
    }
}
