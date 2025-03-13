namespace Task13_20250313
{
    /*
     Написать программу, содержащую многоуровневую структуру данных. 
    Описать классы: «Страна», «Аэропорт», «Самолет», «Характеристики самолета». 
    Реализовать возможность получение полных данных, а самолете 
    (сам самолет, его характеристики, аэропорт в котором он находится, 
    и страна в которой находится аэропорт). Задачу можно реализовать, 
    используя методы Include / ThenInclude или Lazy Loading. В основной части программы, 
    реализовать возможности: 

Добавление страны.
Добавление аэропорта.
Добавление самолета и его характеристик.
Получение полных данных через самолет.
Получение полных данных через страну и аэропорт.
     
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Country> countries = new List<Country>()
            //{
            //    new Country(){Name = "Canada"},
            //    new Country(){Name = "UK"}
            //};

            //List<Airport> airports = new List<Airport>()
            //{
            //    new Airport(){Name = "Toronto Pearson Airport", Country = countries[0]},
            //    new Airport(){Name = "Calgary Airport", Country = countries[0]},
            //    new Airport(){Name = "London Heathrow Airport", Country = countries[1]},
            //    new Airport(){Name = "Cardiff Airport", Country = countries[1]},
            //};

            //List<AircraftSpecs> aircraftSpecs = new List<AircraftSpecs>()
            //{
            //    new AircraftSpecs(){Weight = 100, Speed = 100},
            //    new AircraftSpecs(){Weight = 200, Speed = 200},
            //    new AircraftSpecs(){Weight = 300, Speed = 300},
            //    new AircraftSpecs(){Weight = 400, Speed = 400},
            //    new AircraftSpecs(){Weight = 500, Speed = 500},
            //    new AircraftSpecs(){Weight = 600, Speed = 600},
            //    new AircraftSpecs(){Weight = 700, Speed = 700}
            //};

            //List<Aircraft> aircrafts = new List<Aircraft>()
            //{
            //    new Aircraft(){Name = "B-757", AircraftSpecs=aircraftSpecs[0], Airport = airports[0]},
            //    new Aircraft(){Name = "B-767", AircraftSpecs=aircraftSpecs[1], Airport = airports[0]},
            //    new Aircraft(){Name = "B-777", AircraftSpecs=aircraftSpecs[2], Airport = airports[1]},
            //    new Aircraft(){Name = "A-360", AircraftSpecs=aircraftSpecs[3], Airport = airports[1]},
            //    new Aircraft(){Name = "A-350", AircraftSpecs=aircraftSpecs[4], Airport = airports[2]},
            //    new Aircraft(){Name = "A-340", AircraftSpecs=aircraftSpecs[5], Airport = airports[3]},
            //    new Aircraft(){Name = "A-330", AircraftSpecs=aircraftSpecs[6], Airport = airports[3]}
            //};

            using (ApplicationContext db = new ApplicationContext())
            {
                //db.Countries.AddRange(countries);
                //db.Airports.AddRange(airports);
                //db.AircraftSpecs.AddRange(aircraftSpecs);
                //db.Aircrafts.AddRange(aircrafts);
                //db.SaveChanges();
            }


        }

        /*
         Реализовать возможность получение полных данных, а самолете 
    (сам самолет, его характеристики, аэропорт в котором он находится, 
    и страна в которой находится аэропорт). Задачу можно реализовать, 
    используя методы Include / ThenInclude или Lazy Loading.
         */
        public static void GetAircrafts(int aircraftId, ApplicationContext db)
        {
            Aircraft? aircraft = db.Aircrafts.Where(a => a.Id == aircraftId).FirstOrDefault();
                

        }
    }
}
