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
            }

        }
    }
}
