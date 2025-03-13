using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task13_20250313
{
    /*
     Описать классы: «Страна», «Аэропорт», «Самолет», «Характеристики самолета». 
    Реализовать возможность получение полных данных, а самолете 
    (сам самолет, его характеристики, аэропорт в котором он находится, 
    и страна в которой находится аэропорт). Задачу можно реализовать, 
    используя методы Include / ThenInclude или Lazy Loading.
     */
    public class Aircraft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AirportId { get; set; } // foreign key
        public virtual Airport Airport { get; set; } // nav prop
        public virtual AircraftSpecs AircraftSpecs { get; set; } // nav prop
    }
}
