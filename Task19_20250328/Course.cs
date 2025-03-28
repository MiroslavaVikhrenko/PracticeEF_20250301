using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task19_20250328
{
    /*
     Student (Студент): содержит данные о студентах, включая Id, Name, 
    DateOfBirth (дата рождения) и список курсов, на которые студент записан. 
Course (Курс): содержит данные о курсах, такие как Id, Title, 
    Credits и список студентов, записанных на курс. 
Enrollment (Запись): представляет собой связь между студентом и курсом, 
    содержит StudentId, CourseId, Grade (оценка студента за курс).
     */
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<Enrollment> Enrollments { get; set; } = new(); // nav prop
    }
}
