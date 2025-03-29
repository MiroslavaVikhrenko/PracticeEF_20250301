using Microsoft.EntityFrameworkCore;

namespace Task20_20250329
{
    /*
         Создать таблицы: «Станция» и «Поезд». Используя метод FromSqlRaw и ExecuteSqlRaw, 
        выполнить 8 запросов для получения данных:

Добавить данные про станции и поезда.
Поезда у которых длительность маршрута более 5 часов.
Общую информация о станции и ее поездах.
Название станций у которой в наличии более 3-ех поездов.
Все поезда, модель которых начинается на подстроку «Pell».
Все поезда, у которых возраст более 15 лет с текущей даты.
Получить станции, у которых в наличии хотя бы один поезд с длительность маршрутка менее 4 часов.
Вывести все станции без поездов (на которых не будет поездов при выполнении LEFT JOIN).
         */
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    List<Station> stations = new List<Station>()
            //    {
            //        new Station(){Name = "Calgary"},
            //        new Station(){Name = "Edmonton"}
            //    };
            //    db.Stations.AddRange(stations);
            //    db.SaveChanges();

            //    List<Train> trains = new List<Train>()
            //    {
            //        new Train(){Number = "347", Model = "M1", TravelTime = new TimeSpan(5,30, 10), ManufacturingDate = new DateOnly(2017, 03, 01), StationId = 1},
            //        new Train(){Number = "348", Model = "M2", TravelTime = new TimeSpan(4,30, 10), ManufacturingDate = new DateOnly(2019, 03, 01), StationId = 1},
            //        new Train(){Number = "349", Model = "M1", TravelTime = new TimeSpan(2,30, 10), ManufacturingDate = new DateOnly(2021, 03, 01), StationId = 2},
            //        new Train(){Number = "350", Model = "M2", TravelTime = new TimeSpan(6,30, 10), ManufacturingDate = new DateOnly(2023, 03, 01), StationId = 2},
            //        new Train(){Number = "351", Model = "M1", TravelTime = new TimeSpan(1,30, 10), ManufacturingDate = new DateOnly(2015, 03, 01), StationId = 1},
            //    };
            //    db.Trains.AddRange(trains);
            //    db.SaveChanges();               
            //}

            // Добавить данные про станции.
            Station s = new Station() { Name = "Vancouver"};
            //AddStation(s);

            // Добавить данные про поезда.
            Train t = new Train() { Number = "352", Model = "M3", TravelTime = new TimeSpan(5, 30, 10), ManufacturingDate = new DateOnly(2021, 03, 01), StationId = 1 };
            //AddTrain(t);

            // Поезда у которых длительность маршрута более 5 часов.
            GetTrainsWithLongTravelTime();

            // Общую информация о станции и ее поездах.
            PrintAllStationsWithTrains();
        }

        // Общую информация о станции и ее поездах.
        public static void PrintAllStationsWithTrains()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var stationsWithTrains = db.Stations
                    .FromSqlRaw(@"
                SELECT s.Id, s.Name 
                FROM [Stations] s")
                    .ToList();

                Console.WriteLine("\n----------------------------------\n");

                foreach (var station in stationsWithTrains)
                {
                    Console.WriteLine($"\nStation ID: {station.Id}, Name: {station.Name}");
                    Console.WriteLine("Trains at this station:");

                    var trains = db.Trains
                        .FromSqlRaw(@"
                    SELECT t.* 
                    FROM [Trains] t
                    WHERE t.StationId = {0}", station.Id)
                        .ToList();

                    if (trains.Count == 0)
                    {
                        Console.WriteLine("   No trains available.");
                    }
                    else
                    {
                        foreach (var train in trains)
                        {
                            Console.WriteLine($"   Train ID: {train.Id}, Number: {train.Number}, Model: {train.Model}, " +
                                              $"Manufacturing Date: {train.ManufacturingDate}, Travel Time: {train.TravelTime}");
                        }
                    }
                }
            }
        }

        // Поезда у которых длительность маршрута более 5 часов.
        public static void GetTrainsWithLongTravelTime()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // The startDate ('00:00:00') represents the zero-point in a day.
                var trains = db.Trains
            .FromSqlRaw(@"
                SELECT t.*, s.Name AS StationName 
                FROM [Trains] t
                JOIN [Stations] s ON t.StationId = s.Id
                WHERE DATEDIFF(HOUR, '00:00:00', t.[TravelTime]) > 3")
            .ToList();

                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Trains with Travel Time > 3 hours:");
                foreach (var train in trains)
                {
                    Console.WriteLine($"Train Number: {train.Number}, Model: {train.Model}, Travel Time: {train.TravelTime}");
                }
            }
        }
        // Добавить данные про станции и поезда.
        public static void AddTrain(Train train)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlRaw(
                    "INSERT INTO [Trains] ([Number], [Model], [TravelTime], [ManufacturingDate], [StationId]) VALUES ({0}, {1}, {2}, {3}, {4})",
                    train.Number, train.Model, train.TravelTime, train.ManufacturingDate, train.StationId
                );
            }
        }
        public static void AddStation(Station station)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlRaw("INSERT INTO [Stations] ([Name]) VALUES ({0})", station.Name);
            }
        }
    }

    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Train> Trains { get; set; }
    }
    public class Train
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateOnly ManufacturingDate { get; set; }

        public int StationId { get; set; }
        public Station Station { get; set; }
    }
}
