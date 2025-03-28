using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18_20250328
{
    /*
     Определите следующие классы:
■ Student (Студент): Информация о студентах, включая их идентификатор, имя, фамилию и дату рождения.
■ Course (Курс): Информация о курсах, включая их идентификатор, название и описание.
■ Enrollment (Зачисление): Информация о зачислении студентов на курсы, включая идентификатор зачисления, 
    идентификатор студента, идентификатор курса и дату зачисления.
■ Instructor (Преподаватель): Информация о преподавателях, включая их идентификатор, имя и фамилию.
     */
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Instructor Instructor { get; set; } // nav prop
        public int InstructorId { get; set; } // foreign key

        public virtual List<Enrollment> Enrollments { get; set; } = new(); // nav prop
    }
}
