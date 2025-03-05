using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5_20250305
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Hashed_Password { get; set; }
        public string Salt { get; set; }
    }
}
