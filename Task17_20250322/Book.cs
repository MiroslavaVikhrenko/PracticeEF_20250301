using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task17_20250322
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; } // foreign key
        public int GenreId { get; set; } // foreign key
        public decimal Price { get; set; }
        public virtual Author Author { get; set; } // nav prop
        public virtual Genre Genre { get; set; } // nav prop
    }
}
