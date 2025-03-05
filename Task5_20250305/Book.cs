using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5_20250305
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return $"{Id}.{Title} by {Author} \nDescription: {Description}";
        }
    }
}
