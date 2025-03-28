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
    public class Enrollment
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; } // nav prop
        public int StudentId { get; set; } // foreign key
        public virtual Course Course { get; set; } // nav prop
        public int CourseId { get; set; } // foreign key
        public int Grade { get; set; }
    }
}
