namespace Task20_20250329
{
    internal class Program
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
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Station> stations = new List<Station>()
                {
                    new Station(){Name = "Calgary"},
                    new Station(){Name = "Edmonton"}
                };
                db.Stations.AddRange(stations);
                db.SaveChanges();

                List<Train> trains = new List<Train>()
                {
                    new Train(){Number = "347", Model = "M1", TravelTime = new TimeSpan(5,30, 10), ManufacturingDate = new DateOnly(2017, 03, 01), StationId = 1},
                    new Train(){Number = "348", Model = "M2", TravelTime = new TimeSpan(4,30, 10), ManufacturingDate = new DateOnly(2019, 03, 01), StationId = 1},
                    new Train(){Number = "349", Model = "M1", TravelTime = new TimeSpan(2,30, 10), ManufacturingDate = new DateOnly(2021, 03, 01), StationId = 2},
                    new Train(){Number = "350", Model = "M2", TravelTime = new TimeSpan(6,30, 10), ManufacturingDate = new DateOnly(2023, 03, 01), StationId = 2},
                    new Train(){Number = "351", Model = "M1", TravelTime = new TimeSpan(1,30, 10), ManufacturingDate = new DateOnly(2015, 03, 01), StationId = 1},
                };
                db.Trains.AddRange(trains);
                db.SaveChanges();

                // Добавить данные про станции и поезда.
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
