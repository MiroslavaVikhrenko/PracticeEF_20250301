namespace Task19_20250328
{
    /*
     У вас есть база данных для системы управления университетом. Существуют следующие сущности: 

Student (Студент): содержит данные о студентах, включая Id, Name, DateOfBirth (дата рождения) и список курсов, на которые студент записан. 
Course (Курс): содержит данные о курсах, такие как Id, Title, Credits и список студентов, записанных на курс. 
Enrollment (Запись): представляет собой связь между студентом и курсом, содержит StudentId, CourseId, Grade (оценка студента за курс).

Заполните таблицы данными выполните следующие запросы:

1) Список студентов и количество курсов, на которые они записаны.
2) Курсы, на которые записано больше 10 студентов.
3) Средняя оценка студента по всем курсам.
4) Студенты, которые не записаны ни на один курс.
5) Лучший студент по конкретному курсу (на основе оценки).
6) Количество курсов, на которые записаны студенты старше 30 лет.
7) Курсы с максимальной и минимальной средней оценкой.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Student> students = new List<Student>()
                {
                    new Student(){Name = "Taro", DateOfBirth = new DateTime(2000,04,30)},
                    new Student(){Name = "Keisuke", DateOfBirth = new DateTime(2001,01,20)},
                    new Student(){Name = "Satoshi", DateOfBirth = new DateTime(1999,02,10)},
                    new Student(){Name = "Hiroo", DateOfBirth = new DateTime(1998,03,12)},
                    new Student(){Name = "Hiroshi", DateOfBirth = new DateTime(2003,05,11)},
                    new Student(){Name = "Takeshi", DateOfBirth = new DateTime(2002,06,27)},
                    new Student(){Name = "Kenji", DateOfBirth = new DateTime(2004,07,19)},
                    new Student(){Name = "Midori", DateOfBirth = new DateTime(2005,08,25)},
                    new Student(){Name = "Yuka", DateOfBirth = new DateTime(2006,09,21)},
                    new Student(){Name = "Akiko", DateOfBirth = new DateTime(2005,10,15)},
                    new Student(){Name = "Ken", DateOfBirth = new DateTime(2003,11,12)},
                    new Student(){Name = "Fuyuko", DateOfBirth = new DateTime(2007,12,19)}
                };
                db.Students.AddRange(students);
                db.SaveChanges();

                List<Course> courses = new List<Course>()
                {
                    new Course(){Title = "C#"},
                    new Course(){Title = "C++"},
                    new Course(){Title = "Maths"},
                    new Course(){Title = "Physics"},
                    new Course(){Title = "English"},
                    new Course(){Title = "Presentation"},
                    new Course(){Title = "Japanese"},
                    new Course(){Title = "Databases"},
                    new Course(){Title = "Design patterns"},
                    new Course(){Title = "Coding"}
                };

                db.Courses.AddRange(courses);
                db.SaveChanges();

                List<Enrollment> enrollments = new List<Enrollment>()
                {
                    new Enrollment(){StudentId = 1, CourseId = 1, Grade = 85},
                    new Enrollment(){StudentId = 1, CourseId = 3, Grade = 72},
                    new Enrollment(){StudentId = 1, CourseId = 10, Grade = 91},
                    new Enrollment(){StudentId = 2, CourseId = 2, Grade = 64},
                    new Enrollment(){StudentId = 2, CourseId = 6, Grade = 99},
                    new Enrollment(){StudentId = 2, CourseId = 9, Grade = 77},
                    new Enrollment(){StudentId = 3, CourseId = 3, Grade = 88},
                    new Enrollment(){StudentId = 3, CourseId = 7, Grade = 93},
                    new Enrollment(){StudentId = 3, CourseId = 9, Grade = 69},
                    new Enrollment(){StudentId = 4, CourseId = 4, Grade = 90},
                    new Enrollment(){StudentId = 4, CourseId = 5, Grade = 78},
                    new Enrollment(){StudentId = 4, CourseId = 10, Grade = 82},
                    new Enrollment(){StudentId = 5, CourseId = 2, Grade = 95},
                    new Enrollment(){StudentId = 6, CourseId = 1, Grade = 87},
                    new Enrollment(){StudentId = 6, CourseId = 6, Grade = 66},
                    new Enrollment(){StudentId = 6, CourseId = 10, Grade = 80},
                    new Enrollment(){StudentId = 7, CourseId = 3, Grade = 74},
                    new Enrollment(){StudentId = 7, CourseId = 5, Grade = 99},
                    new Enrollment(){StudentId = 8, CourseId = 1, Grade = 92},
                    new Enrollment(){StudentId = 8, CourseId = 2, Grade = 63},
                    new Enrollment(){StudentId = 8, CourseId = 3, Grade = 89},
                    new Enrollment(){StudentId = 9, CourseId = 1, Grade = 79},
                    new Enrollment(){StudentId = 9, CourseId = 4, Grade = 94},
                    new Enrollment(){StudentId = 9, CourseId = 8, Grade = 67},
                    new Enrollment(){StudentId = 10, CourseId = 2, Grade = 76},
                    new Enrollment(){StudentId = 10, CourseId = 3, Grade = 85},
                    new Enrollment(){StudentId = 10, CourseId = 7, Grade = 98},
                    new Enrollment(){StudentId = 10, CourseId = 8, Grade = 81}
                };
                db.Enrollments.AddRange(enrollments);
                db.SaveChanges();

                // 1) Список студентов и количество курсов, на которые они записаны.
                PrintStudentsWithCourses(db);

                // 2) Курсы, на которые записано больше 10 студентов.
                PrintPopularCourses(db);

                // 3) Средняя оценка студента по всем курсам.
                PrintStudentsWithGrades(db);

                // 4) Студенты, которые не записаны ни на один курс.
                PrintStudentsWithoutCourses(db);

                // 5) Лучший студент по конкретному курсу (на основе оценки).
                PrintTopStudentInCourse(db, 1);
            }
        }
        // 5) Лучший студент по конкретному курсу (на основе оценки).
        public static void PrintTopStudentInCourse(ApplicationContext db, int courseId)
        {
            var course = db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    c.Title,
                    TopStudent = c.Enrollments
                        .OrderByDescending(e => e.Grade) // Sort by highest grade
                        .Select(e => new
                        {
                            e.Student.Name,
                            e.Grade
                        })
                        .FirstOrDefault() // Take the top student
                })
                .FirstOrDefault();

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine($"Course: {course.Title}");

            if (course.TopStudent != null)
            {
                Console.WriteLine("Top Student:");
                Console.WriteLine($" - {course.TopStudent.Name}, Grade: {course.TopStudent.Grade}");
            }
            else
            {
                Console.WriteLine("No students are enrolled in this course.");
            }
        }
        // 4) Студенты, которые не записаны ни на один курс.
        public static void PrintStudentsWithoutCourses(ApplicationContext db)
        {
            var studentsWithoutCourses = db.Students
                .Where(s => !s.Enrollments.Any()) // Students with no enrollments
                .Select(s => s.Name) // Only select the Name
                .ToList();

            if (studentsWithoutCourses.Any())
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Students not enrolled in any courses:");
                foreach (var student in studentsWithoutCourses)
                {
                    Console.WriteLine($" - {student}");
                }
            }
            else
            {
                Console.WriteLine("All students are enrolled in at least one course.");
            }
        }
        // 3) Средняя оценка студента по всем курсам.
        public static void PrintStudentsWithGrades(ApplicationContext db)
        {
            var students = db.Students
                .Where(s => s.Enrollments.Any()) // Only students with enrollments
                .Select(s => new
                {
                    s.Name,
                    AverageGrade = s.Enrollments.Average(e => e.Grade),
                    Courses = s.Enrollments.Select(e => new
                    {
                        e.Course.Title,
                        e.Grade
                    }).ToList()
                })
                .ToList();

            Console.WriteLine("\n----------------------------------\n");

            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}");
                Console.WriteLine($"Average Grade: {student.AverageGrade:F2}");

                Console.WriteLine("Enrolled Courses:");
                foreach (var course in student.Courses)
                {
                    Console.WriteLine($" - {course.Title}, Grade: {course.Grade}");
                }

                Console.WriteLine("\n-----\n");
            }
        }

        // 2) Курсы, на которые записано больше 10 студентов.
        public static void PrintPopularCourses(ApplicationContext db)
        {
            var courses = db.Courses
                .Where(c => c.Enrollments.Count() > 3) // Only courses with more than 3 students
                .Select(c => new
                {
                    c.Title,
                    StudentCount = c.Enrollments.Count(),
                    Students = c.Enrollments.Select(e => new
                    {
                        e.Student.Name,
                        e.Grade
                    }).ToList()
                })
                .ToList();

            Console.WriteLine("\n----------------------------------\n");

            foreach (var course in courses)
            {
                Console.WriteLine($"Course: {course.Title}");
                Console.WriteLine($"Total Students Enrolled: {course.StudentCount}");

                Console.WriteLine("Enrolled Students:");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($" - {student.Name}, Grade: {student.Grade}");
                }

                Console.WriteLine("\n-----\n");
            }
        }

        // 1) Список студентов и количество курсов, на которые они записаны.
        public static void PrintStudentsWithCourses(ApplicationContext db)
        {
            var students = db.Students
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    Courses = s.Enrollments.Select(e => new
                    {
                        e.Course.Title,
                        e.Grade
                    }).ToList()
                })
                .ToList();
            Console.WriteLine("\n----------------------------------\n");
            foreach (var student in students)
            {
                Console.WriteLine($"Student ID: {student.Id}, Name: {student.Name}");
                Console.WriteLine($"Total Courses Enrolled: {student.Courses.Count}");

                if (student.Courses.Any())
                {
                    Console.WriteLine("Courses & Grades:");
                    foreach (var course in student.Courses)
                    {
                        Console.WriteLine($" - {course.Title}, Grade: {course.Grade}");
                    }
                }
                else
                {
                    Console.WriteLine("No enrolled courses.");
                }

                Console.WriteLine("\n-----\n"); 
            }
        }
    }
}
