using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Task15_20250316
{
    /*
     Создайте систему для управления «Событиями» и «Гостями». 
Создайте таблицы для событий, гостей и связывающую таблицу, 
    которая представляет собой отношение «многие ко многим» между событиями и гостями. 
    Дополнительно в этой связи создать дополнительную колонку для хранения «Роли гостя» на конкретном событии.
Выполните следующие запросы к созданным таблицам (каждый запрос оформите в отдельный метод):

1) Добавление гостя на событие.
2) Получение списка гостей на конкретном событии.
3) Изменение роли гостя на событии.
4) Получение всех событий для конкретного гостя.
5) Удаление гостя с события.
6) Получение всех событий, на которых гость выступал в роли спикера.
7) * Напишите запрос, который находит Топ 3 гостей, участвовавших в наибольшем количестве событий, 
    и выводит их общее количество участий и список событий, в которых они участвовали.
     */
    internal class Program
    {
        static void Main(string[] args)
        { 
            using (ApplicationContext db = new ApplicationContext())
            {
            //    List<Guest> guests = new List<Guest>()
            //{
            //    new Guest(){Name = "Tanaka"},
            //    new Guest(){Name = "Yamada"},
            //    new Guest(){Name = "Fujita"},
            //    new Guest(){Name = "Hayashi"},
            //    new Guest(){Name = "Sato"},
            //    new Guest(){Name = "Kobayashi"},
            //    new Guest(){Name = "Sasayama"},
            //    new Guest(){Name = "Okuda"},
            //    new Guest(){Name = "Ishikawa"},
            //    new Guest(){Name = "Yamamoto"},
            //};
            //    List<Event> events = new List<Event>()
            //{
            //    new Event(){Name = "Programming conference"},
            //    new Event(){Name = "Cloud conference"},
            //    new Event(){Name = "Azure conference"},
            //    new Event(){Name = "AWS conference"},
            //    new Event(){Name = "Next.js conference"},
            //    new Event(){Name = "DevOps conference"},
            //    new Event(){Name = "JavaScript conference"},
            //    new Event(){Name = "Write lean code conference"},
            //    new Event(){Name = "Performance conference"},
            //    new Event(){Name = "Database conference"}
            //};
            //    db.Guests.AddRange(guests);
            //    db.Events.AddRange(events);
            //    db.SaveChanges();

            //    List<GuestEvent> guestEvents = new List<GuestEvent>()
            //{
            //    new GuestEvent(){Role = Role.Speaker, EventId=1, GuestId=1},
            //    new GuestEvent(){Role = Role.Manager, EventId=1, GuestId=2},
            //    new GuestEvent(){Role = Role.Visitor, EventId=1, GuestId=5},
            //    new GuestEvent(){Role = Role.Speaker, EventId=2, GuestId=1},
            //    new GuestEvent(){Role = Role.Visitor, EventId=2, GuestId=5},
            //    new GuestEvent(){Role = Role.Visitor, EventId=2, GuestId=7},
            //    new GuestEvent(){Role = Role.Manager, EventId=2, GuestId=10},
            //    new GuestEvent(){Role = Role.Speaker, EventId=3, GuestId=2},
            //    new GuestEvent(){Role = Role.Visitor, EventId=3, GuestId=1},
            //    new GuestEvent(){Role = Role.Speaker, EventId=4, GuestId=1},
            //    new GuestEvent(){Role = Role.Speaker, EventId=5, GuestId=7},
            //    new GuestEvent(){Role = Role.Manager, EventId=10, GuestId=8},
            //    new GuestEvent(){Role = Role.Speaker, EventId=6, GuestId=9},
            //    new GuestEvent(){Role = Role.Speaker, EventId=7, GuestId=10},
            //    new GuestEvent(){Role = Role.Visitor, EventId=8, GuestId=5},
            //    new GuestEvent(){Role = Role.Speaker, EventId=8, GuestId=4},
            //    new GuestEvent(){Role = Role.Manager, EventId=10, GuestId=6},
            //    new GuestEvent(){Role = Role.Speaker, EventId=9, GuestId=6},
            //    new GuestEvent(){Role = Role.Speaker, EventId=8, GuestId=6},
            //    new GuestEvent(){Role = Role.Speaker, EventId=7, GuestId=6}
            //};
            //    db.GuestEvents.AddRange(guestEvents);
            //    db.SaveChanges();

                //AddGuestToEvent(5, 5, Role.Speaker, db);
                GetGuestsFromEvent(1, db);
                //UpdateRoleForEvent(1, Role.Speaker, db);
                GetEventsForGuest(1, db);
                //DeleteGuestFromEvent(20, db);
                FindSpeakerEventsForGuest(1, db);
                FindTopThreeGuests();
            }

        }
        /* Напишите запрос, который находит Топ 3 гостей, участвовавших в наибольшем количестве событий, 
    и выводит их общее количество участий и список событий, в которых они участвовали.
     */
        public static void FindTopThreeGuests()
        {            
            Console.WriteLine("--------\nTop 3 guests:");
            using (SqlConnection connection = new SqlConnection("Data Source=MIRUAHUA;Initial Catalog=March_EventsForGuests;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                connection.Open();
                string commandtext = $"""
                    SELECT TOP 3 
                        g.Id AS GuestId, 
                        g.Name AS GuestName, 
                        COUNT(ge.EventId) AS EventCount
                    FROM [March_EventsForGuests].[dbo].[GuestEvents] ge
                    JOIN [March_EventsForGuests].[dbo].[Guests] g 
                        ON ge.GuestId = g.Id
                    GROUP BY g.Id, g.Name
                    ORDER BY EventCount DESC;
                    """;
                SqlCommand command = new SqlCommand(commandtext, connection);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);
                        Console.WriteLine($"> {columnName1} | {columnName2} | {columnName3}");
                        while (reader.Read())
                        {
                            int guestId = reader.GetInt32(0);
                            string guestName = reader.GetString(1);
                            int events = reader.GetInt32(2);

                            Console.WriteLine($">>> {guestId} | {guestName} | {events}");
                        }
                    }
                }

            }
        }

        // Получение всех событий, на которых гость выступал в роли спикера.
        public static void FindSpeakerEventsForGuest(int guestId, ApplicationContext db)
        {
            Guest? g = db.Guests
            .Where(g => g.Id == guestId)
            .Include(g => g.GuestEvents) // Include GuestEvents join table
            .ThenInclude(ge => ge.Event) // Then include the Events
            .FirstOrDefault();

            if (g == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            Console.WriteLine($"-----\nSPEAKER Events for guest {g.Name}:");
            foreach (GuestEvent? ge in g.GuestEvents)
            {
                if (ge.Role == Role.Speaker)
                {
                    Console.WriteLine($">>> {ge.Event?.Name} ({ge.Role})");
                }           
            }
        }

        // Удаление гостя с события.
        public static void DeleteGuestFromEvent(int guesteventid, ApplicationContext db)
        {
            GuestEvent? guestEvent = db.GuestEvents.Where(e => e.Id == guesteventid).FirstOrDefault();
            if (guestEvent == null)
            {
                Console.WriteLine("Registration to Event not found.");
                return;
            }
            db.GuestEvents.Remove(guestEvent);
            db.SaveChanges();
        }

        // Получение всех событий для конкретного гостя.
        public static void GetEventsForGuest(int guestId, ApplicationContext db)
        {
            Guest? g = db.Guests
            .Where(g => g.Id == guestId)
            .Include(g => g.GuestEvents) // Include GuestEvents join table
            .ThenInclude(ge => ge.Event) // Then include the Events
            .FirstOrDefault();

            if (g == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            Console.WriteLine($"-----\nEvents for guest {g.Name}:");
            foreach (GuestEvent? ge in g.GuestEvents)
            {
                Console.WriteLine($">>> {ge.Event?.Name} ({ge.Role})");
            }
        }

        // Изменение роли гостя на событии.
        public static void UpdateRoleForEvent(int guesteventid, Role newrole, ApplicationContext db)
        {
            GuestEvent? guestEvent = db.GuestEvents.Where(e => e.Id == guesteventid).FirstOrDefault();
            if (guestEvent == null)
            {
                Console.WriteLine("Registration to Event not found.");
                return;
            }
            guestEvent.Role = newrole;
            db.SaveChanges();
        }
        // Получение списка гостей на конкретном событии.
        public static void GetGuestsFromEvent(int eventId, ApplicationContext db)
        {
            Event? e = db.Events
            .Where(e => e.Id == eventId)
            .Include(e => e.GuestEvents) // Include GuestEvents join table
            .ThenInclude(ge => ge.Guest) // Then include the Guests
            .FirstOrDefault();

            if (e == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }

            Console.WriteLine($"-----\nGuests for event {e.Name}:");
            foreach (GuestEvent? ge in e.GuestEvents)
            {
                Console.WriteLine($">>> {ge.Guest?.Name} ({ge.Role})");
            }
        }

        // Добавление гостя на событие.
        public static void AddGuestToEvent(int guestId, int eventId, Role role, ApplicationContext db)
        {
            db.GuestEvents.Add(new GuestEvent { GuestId = guestId, EventId = eventId, Role = role });
            db.SaveChanges();
            Console.WriteLine($"------------\nGuest {guestId} is added as {role} to event {eventId}");
        }
    }
}
