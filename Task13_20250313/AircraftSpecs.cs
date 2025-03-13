using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class AircraftSpecs
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Speed { get; set; }
        public virtual Aircraft Aircraft { get; set; } //nav prop
        [ForeignKey("AircraftId")]
        public int AircraftId { get; set; } // foreign key
        
    }
}
