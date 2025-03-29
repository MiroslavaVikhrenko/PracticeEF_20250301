using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
     Используя таблицы «Users» и «Companies», создать 3 хранимых процедуры:
Получение связанных данных о Users и Companies.
Используя входной параметр получить пользователей с именем наподобие “Tom”.
Используя выходной параметр, получить средний возраст по всей таблицы пользователей.
     */
namespace Task21_20250329
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; } // nav prop
        public int CompanyId { get; set; } // foreign key
    }
}
