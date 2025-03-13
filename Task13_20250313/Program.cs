using System.Diagnostics.Metrics;

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

                GetAircrafts(1, db);
                //AddCountry("Germany", db);
                //AddAirport("Charles de Gaulle Airport", 4, db);
                //AddAircraftsAndSpecs("B-367", 800, 800, 5, db);
                //GetAircraftDetailsByCountry("UK", db); // ???
                GetAircraftDetailsByAirport(2, db);
            }


        }

        //Получение полных данных через страну и аэропорт.

        public static void GetAircraftDetailsByAirport(int airportId, ApplicationContext db)
        {
            Console.WriteLine("-------------");
            Airport? airport = db.Airports.Where(a => a.Id == airportId).FirstOrDefault();
            if (airport!= null)
            {
                Console.WriteLine($"Airport: {airport.Name} in {airport.Country.Name}");
                foreach (Aircraft aircraft in airport.Aircrafts)
                {
                    Console.WriteLine($">>> {aircraft.Name} (specs: weight {aircraft.AircraftSpecs.Weight}, speed {aircraft.AircraftSpecs.Speed}");
                }
            }
        }

        public static void GetAircraftDetailsByCountry(string countryName, ApplicationContext db)
        {
            Console.WriteLine("-------------");
            Country? country = db.Countries.Where(c => c.Name == countryName).FirstOrDefault();
            if (country != null)
            {
                Console.WriteLine($"Country: {country.Name}");
                foreach (Airport airport in db.Airports)
                {
                    Console.WriteLine($">>> {airport.Name}");
                    foreach(Aircraft aircraft in airport.Aircrafts)
                    {
                        Console.WriteLine($">>>>>>> {aircraft.Name} (specs: weight {aircraft.AircraftSpecs.Weight}, speed {aircraft.AircraftSpecs.Speed}");
                    }
                }
            }
        }

        //Добавление самолета и его характеристик.

        public static void AddAircraftsAndSpecs(string aircraftName, int weight, int speed, int airportId, ApplicationContext db)
        {
            Aircraft aircraft = new Aircraft()
            {
                Name = aircraftName,
                AircraftSpecs = new AircraftSpecs() { Weight = weight, Speed = speed },
                AirportId = airportId
            };
            db.Aircrafts.Add(aircraft);
            db.SaveChanges();

            List<Aircraft> aircrafts = db.Aircrafts.ToList();
            Console.WriteLine("-------------\nAircrafts in the db:");
            foreach (Aircraft a in aircrafts)
            {
                Console.WriteLine($">> {a.Name} (Specs: weight {a.AircraftSpecs.Weight}, speed {a.AircraftSpecs.Speed})");
            }
        }

        // Добавление аэропорта. 

        public static void AddAirport(string airportName, int countryId, ApplicationContext db)
        {
            Airport airport = new Airport() { Name = airportName, CountryId = countryId};
            db.Airports.Add(airport);
            db.SaveChanges();

            List<Airport> airports = db.Airports.ToList();
            Console.WriteLine("-------------\nAirports in the db:");
            foreach (Airport a in airports)
            {
                Console.WriteLine($">> {a.Name} ({a.Country.Name})");
            }
        }

        // Добавление страны.

        public static void AddCountry(string countryName, ApplicationContext db)
        {
            Country country = new Country() { Name = countryName };
            db.Countries.Add(country);
            db.SaveChanges();

            List<Country> countries = db.Countries.ToList();
            Console.WriteLine("-------------\nCountries in the db:");
            foreach(Country c in countries)
            {
                Console.WriteLine($">> {c.Name}");
            }
        }
        //Получение полных данных через самолет.
        /*
         Реализовать возможность получение полных данных, а самолете 
    (сам самолет, его характеристики, аэропорт в котором он находится, 
    и страна в которой находится аэропорт). Задачу можно реализовать, 
    используя методы Include / ThenInclude или Lazy Loading.
         */
        public static void GetAircrafts(int aircraftId, ApplicationContext db)
        {
            Aircraft? aircraft = db.Aircrafts.Where(a => a.Id == aircraftId).FirstOrDefault();
            if (aircraft != null)
            {
                Console.WriteLine($"Aircraft {aircraft.Name},\nairport {aircraft.Airport.Name} in {aircraft.Airport.Country.Name},\nspecs: weight {aircraft.AircraftSpecs.Weight}, speed {aircraft.AircraftSpecs.Speed} ");
            }
            
        }
    }
}
